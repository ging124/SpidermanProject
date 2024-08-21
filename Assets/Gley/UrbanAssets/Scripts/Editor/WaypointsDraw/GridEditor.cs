﻿#if GLEY_PEDESTRIAN_SYSTEM
using Gley.PedestrianSystem;
using Gley.PedestrianSystem.Internal;
using Gley.PedestrianSystem.Editor;
#endif
#if GLEY_TRAFFIC_SYSTEM
using Gley.TrafficSystem.Editor;
using Gley.TrafficSystem.Internal;
#endif
using Gley.UrbanAssets.Internal;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Gley.UrbanAssets.Editor
{
    public class GridEditor : UnityEditor.Editor
    {

        #region BUTTONS
        //Called by Apply Settings button
        public static void ApplySettings(CurrentSceneData currentSceneData)
        {
#if GLEY_PEDESTRIAN_SYSTEM
            AssignPedestrianWaypoints(currentSceneData);
#endif

#if GLEY_TRAFFIC_SYSTEM
            AssignTrafficWaypoints(currentSceneData);
#endif
        }


        //Called by Generate Grid button
        public static void GenerateGrid(CurrentSceneData currentSceneData)
        {
            System.DateTime startTime = System.DateTime.Now;
            int nrOfColumns;
            int nrOfRows;
            Bounds b = new Bounds();
            foreach (Renderer r in FindObjectsByType<Renderer>(FindObjectsSortMode.None))
            {
                b.Encapsulate(r.bounds);
            }
            foreach (Terrain t in FindObjectsByType<Terrain>(FindObjectsSortMode.None))
            {
                b.Encapsulate(t.terrainData.bounds);
            }

            nrOfColumns = Mathf.CeilToInt(b.size.x / currentSceneData.gridCellSize);
            nrOfRows = Mathf.CeilToInt(b.size.z / currentSceneData.gridCellSize);
            if (nrOfRows == 0 || nrOfColumns == 0)
            {
                Debug.LogError("Your scene seems empty. Please add some geometry inside your scene before setting up traffic");
                return;
            }
            Debug.Log("Center: " + b.center + " size: " + b.size + " nrOfColumns " + nrOfColumns + " nrOfRows " + nrOfRows);
            Vector3 corner = new Vector3(b.center.x - b.size.x / 2 + currentSceneData.gridCellSize / 2, 0, b.center.z - b.size.z / 2 + currentSceneData.gridCellSize / 2);
            int nr = 0;
            currentSceneData.grid = new GridRow[nrOfRows];
            for (int row = 0; row < nrOfRows; row++)
            {
                currentSceneData.grid[row] = new GridRow(nrOfColumns);
                for (int column = 0; column < nrOfColumns; column++)
                {
                    nr++;
                    currentSceneData.grid[row].row[column] = new GridCell(column, row, new Vector3(corner.x + column * currentSceneData.gridCellSize, 0, corner.z + row * currentSceneData.gridCellSize), currentSceneData.gridCellSize);
                }
            }
            currentSceneData.gridCorner = currentSceneData.grid[0].row[0].center - currentSceneData.grid[0].row[0].size / 2;
            EditorUtility.SetDirty(currentSceneData);
            Debug.Log("Done generate grid in " + (System.DateTime.Now - startTime));
        }


        private static void SetTags()
        {
            ConnectionPoolBase[] allWaypointHolders = FindObjectsByType<ConnectionPoolBase>(FindObjectsSortMode.None);
            for (int i = 0; i < allWaypointHolders.Length; i++)
            {
                allWaypointHolders[i].gameObject.SetTag(UrbanAssets.Internal.Constants.editorTag);
            }
        }
        #endregion


        #region TRAFFIC

#if GLEY_TRAFFIC_SYSTEM
        static List<WaypointSettings> allEditorWaypoints;
        static GenericIntersectionSettings[] allEditorIntersections;


        static void AssignTrafficWaypoints(CurrentSceneData currentSceneData)
        {
            if (currentSceneData == null || currentSceneData.grid == null || currentSceneData.grid.Length == 0)
            {
                Debug.LogError("Grid is null. Go to Tools->Gley->Traffic System->Scene Setup->Grid Setup and set up your grid");
                return;
            }


            System.DateTime startTime = System.DateTime.Now;
            SetTags();
            ClearAllTrafficWaypoints(currentSceneData);
            List<WaypointSettings> allWaypoints = FindObjectsByType<WaypointSettings>(FindObjectsSortMode.None).ToList();
            if (allWaypoints.Count <= 0)
            {
                Debug.LogWarning("No waypoints found. Go to Tools->Gley->Traffic System->Road Setup and create a road");
                return;
            }


            for (int i = 0; i < allWaypoints.Count; i++)
            {
                //reset intersection waypoints
                allWaypoints[i].enter = allWaypoints[i].exit = false;

                allWaypoints[i].distance = new List<int>();
                for (int j = 0; j < allWaypoints[i].neighbors.Count; j++)
                {
                    if (allWaypoints[i].neighbors[j] != null)
                    {
                        allWaypoints[i].distance.Add((int)Vector3.Distance(allWaypoints[i].transform.position, allWaypoints[i].neighbors[j].transform.position));
                    }
                    else
                    {
                        Debug.LogWarning($"{allWaypoints[i].name} has null neighbors", allWaypoints[i]);
                    }

                }
            }


            allEditorWaypoints = new List<WaypointSettings>();
            allEditorIntersections = FindObjectsByType<GenericIntersectionSettings>(FindObjectsSortMode.None);
            for (int i = 0; i < allEditorIntersections.Length; i++)
            {
                List<IntersectionStopWaypointsSettings> intersectionWaypoints = allEditorIntersections[i].GetAssignedWaypoints();
                if (intersectionWaypoints != null)
                {
                    for (int j = 0; j < intersectionWaypoints.Count; j++)
                    {
                        for (int k = 0; k < intersectionWaypoints[j].roadWaypoints.Count; k++)
                        {
                            if (intersectionWaypoints[j].roadWaypoints[k] == null)
                            {
                                Debug.LogError(allEditorIntersections[i].name + " has null waypoints assigned, please check it.", allEditorIntersections[i]);
                                continue;
                            }
                            else
                            {
                                intersectionWaypoints[j].roadWaypoints[k].enter = true;
                            }
                        }
                    }
                }
                List<WaypointSettings> exitWaypoints = allEditorIntersections[i].GetExitWaypoints();
                if (exitWaypoints != null)
                {
                    for (int j = 0; j < exitWaypoints.Count; j++)
                    {
                        if (exitWaypoints[j] == null)
                        {
                            Debug.LogError(allEditorIntersections[i].name + " has null waypoints assigned, please check it.", allEditorIntersections[i]);
                        }
                        else
                        {
                            exitWaypoints[j].exit = true;
                        }
                    }
                }
            }

            for (int i = allWaypoints.Count - 1; i >= 0; i--)
            {
                if (allWaypoints[i].allowedCars.Count != 0)
                {
                    allEditorWaypoints.Add(allWaypoints[i]);
                    GridCell cell = currentSceneData.GetCell(allWaypoints[i].transform.position);
                    cell.AddTrafficWaypoint(allEditorWaypoints.Count - 1, allWaypoints[i].name, allWaypoints[i].allowedCars.Cast<int>().ToList(), allWaypoints[i].enter || allWaypoints[i].exit, allWaypoints[i].priority);
                }
                if (allWaypoints[i].laneWidth == 0)
                {
#if !GLEY_CIDY_TRAFFIC && !GLEY_EASYROADS_TRAFFIC
                    if (!allWaypoints[i].name.Contains(Gley.UrbanAssets.Internal.Constants.connect))
                    {
                        allWaypoints[i].laneWidth= (allWaypoints[i].transform.parent.parent.parent.GetComponent<Road>().laneWidth);
                    }
                    else
                    {
                        allWaypoints[i].laneWidth =allWaypoints[i].connection.fromRoad.laneWidth;
                    }
#endif
                }
            }

            currentSceneData.allWaypoints = allEditorWaypoints.ToPlayWaypoints(allEditorWaypoints).ToArray();

            //generate path finding waypoints

            TrafficSettingsLoader settingsLoader = new TrafficSettingsLoader(Gley.TrafficSystem.Internal.Constants.windowSettingsPath);
            bool pathfindingEnabled = settingsLoader.LoadPathFindingSettings().enabled;

            if (pathfindingEnabled)
            {
                PathFindingData pathFindingData = currentSceneData.GetComponent<PathFindingData>();
                if (pathFindingData == null)
                {
                    pathFindingData = currentSceneData.gameObject.AddComponent<PathFindingData>();
                }
                pathFindingData.GenerateWaypoints(allEditorWaypoints);
            }
            else
            {
                DestroyImmediate(currentSceneData.GetComponent<PathFindingData>());
            }

            List<int> waypointsToRemove = new List<int>();
            for (int i = 0; i < currentSceneData.allWaypoints.Length; i++)
            {
                for (int j = 0; j < currentSceneData.allWaypoints[i].giveWayList.Count; j++)
                {
                    waypointsToRemove.Add(currentSceneData.allWaypoints[i].giveWayList[j]);
                }
            }


            for (int i = 0; i < currentSceneData.grid.Length; i++)
            {
                for (int j = 0; j < currentSceneData.grid[i].row.Length; j++)
                {
                    currentSceneData.grid[i].row[j].RemoveSpawnWaypoint(waypointsToRemove);
                }
            }
            AssignIntersections(currentSceneData);

            //Assign zipper give way
            for (int i = 0; i < currentSceneData.allWaypoints.Length; i++)
            {
                if (currentSceneData.allWaypoints[i].zipperGiveWay)
                {
                    for (int j = 0; j < currentSceneData.allWaypoints[i].prev.Count; j++)
                    {
                        currentSceneData.allWaypoints[currentSceneData.allWaypoints[i].prev[j]].giveWay = true;
                    }
                }
            }

            EditorUtility.SetDirty(currentSceneData);
            //Debug.Log("Done assign vehicle waypoints in " + (System.DateTime.Now - startTime));
        }



        private static void AssignIntersections(CurrentSceneData currentSceneData)
        {
            List<PriorityIntersection> priorityIntersections = new List<PriorityIntersection>();
            List<TrafficLightsIntersection> lightsIntersections = new List<TrafficLightsIntersection>();
            currentSceneData.allIntersections = new IntersectionData[allEditorIntersections.Length];
            for (int i = 0; i < allEditorIntersections.Length; i++)
            {
                if (allEditorIntersections[i].GetType().Equals(typeof(TrafficLightsIntersectionSettings)))
                {
                    TrafficLightsIntersection intersection = ((TrafficLightsIntersectionSettings)allEditorIntersections[i]).ToPlayModeIntersection(allEditorWaypoints);
#if GLEY_PEDESTRIAN_SYSTEM
                    GetPedestrianWaypoints(intersection, (TrafficLightsIntersectionSettings)allEditorIntersections[i]);
#endif

                    lightsIntersections.Add(intersection);
                    currentSceneData.allIntersections[i] = new IntersectionData(IntersectionType.TrafficLights, lightsIntersections.Count - 1);
                }

                if (allEditorIntersections[i].GetType().Equals(typeof(PriorityIntersectionSettings)))
                {
                    PriorityIntersection intersection = ((PriorityIntersectionSettings)allEditorIntersections[i]).ToPlayModeIntersection(allEditorWaypoints);
#if GLEY_PEDESTRIAN_SYSTEM
                    GetPedestrianWaypoints(intersection, (PriorityIntersectionSettings)allEditorIntersections[i]);
#endif
                    priorityIntersections.Add(intersection);
                    currentSceneData.allIntersections[i] = new IntersectionData(IntersectionType.Priority, priorityIntersections.Count - 1);
                }

                List<IntersectionStopWaypointsSettings> intersectionWaypoints = allEditorIntersections[i].GetAssignedWaypoints();
                for (int j = 0; j < intersectionWaypoints.Count; j++)
                {
                    for (int k = 0; k < intersectionWaypoints[j].roadWaypoints.Count; k++)
                    {
                        if (intersectionWaypoints[j].roadWaypoints[k] == null)
                        {
                            intersectionWaypoints[j].roadWaypoints.RemoveAt(k);
                        }
                        else
                        {
                            GridCell intersectionCell = currentSceneData.GetCell(intersectionWaypoints[j].roadWaypoints[k].transform.position);
                            intersectionCell.AddIntersection(i);
                        }
                    }
                }
            }
            currentSceneData.allPriorityIntersections = priorityIntersections.ToArray();
            currentSceneData.allLightsIntersections = lightsIntersections.ToArray();

        }

        private static void ClearAllTrafficWaypoints(CurrentSceneData currentSceneData)
        {
            if (currentSceneData.grid != null)
            {
                for (int i = 0; i < currentSceneData.grid.Length; i++)
                {
                    for (int j = 0; j < currentSceneData.grid[i].row.Length; j++)
                    {
                        currentSceneData.grid[i].row[j].ClearTrafficReferences();
                    }
                }
            }
        }
#endif

        #endregion


        #region PEDESTRIANS

#if GLEY_PEDESTRIAN_SYSTEM
        static List<PedestrianWaypointSettings> allPedestrianEditorWaypoints;

        static void AssignPedestrianWaypoints(CurrentSceneData currentSceneData)
        {
            if (currentSceneData == null || currentSceneData.grid == null || currentSceneData.grid.Length == 0)
            {
                Debug.LogError("Grid is null. Go to Tools->Gley->Pedestrian System->Scene Setup->Grid Setup and set up your grid");
                return;
            }

            System.DateTime startTime = System.DateTime.Now;
            SetTags();
            ClearPedestrianAllWaypoints(currentSceneData);
            List<PedestrianWaypointSettings> allPedestrianWaypoints = FindObjectsOfType<PedestrianWaypointSettings>().ToList();
            if (allPedestrianWaypoints.Count <= 0)
            {
                Debug.LogWarning("No waypoints found. Go to Tools->Gley->Pedestrian System->Path Setup and create a path");
                return;
            }

            for (int i = 0; i < allPedestrianWaypoints.Count; i++)
            {
                allPedestrianWaypoints[i].inIntersection = false;
                allPedestrianWaypoints[i].stop = false;
                allPedestrianWaypoints[i].crossing = false;
            }

            allPedestrianEditorWaypoints = new List<PedestrianWaypointSettings>();


#if GLEY_TRAFFIC_SYSTEM
            GenericIntersectionSettings[] allIntersections = FindObjectsOfType<GenericIntersectionSettings>();
            //check if a waypoint is in intersection
            for (int i = 0; i < allIntersections.Length; i++)
            {
                if (!allIntersections[i].VerifyAsignements())
                    return;

                List<PedestrianWaypointSettings> intersectionWaypoints = allIntersections[i].GetPedestrianWaypoints();
                if (intersectionWaypoints != null)
                {
                    for (int j = intersectionWaypoints.Count - 1; j >= 0; j--)
                    {
                        if (intersectionWaypoints[j])
                        {
                            intersectionWaypoints[j].inIntersection = true;
                            intersectionWaypoints[j].stop = true;
                        }
                        else
                        {
                            intersectionWaypoints.RemoveAt(j);
                        }
                    }
                }

                List<PedestrianWaypointSettings> directionWaypoints = allIntersections[i].GetDirectionWaypoints();
                if (directionWaypoints != null)
                {
                    for (int j = directionWaypoints.Count - 1; j >= 0; j--)
                    {
                        if (directionWaypoints[j])
                        {
                            directionWaypoints[j].crossing = true;
                        }
                        else
                        {
                            directionWaypoints.RemoveAt(j);
                        }
                    }
                }
            }
#endif

            List<StopWaypointsComponent> allIntersectionComponents = FindObjectsOfType<StopWaypointsComponent>().ToList();
            for (int i = 0; i < allIntersectionComponents.Count; i++)
            {
                if (!allIntersectionComponents[i].VerifyAsignements())
                    return;


                List<PedestrianWaypointSettings> intersectionWaypoints = allIntersectionComponents[i].GetPedestrianWaypoints();
                if (intersectionWaypoints != null)
                {
                    for (int j = intersectionWaypoints.Count - 1; j >= 0; j--)
                    {
                        if (intersectionWaypoints[j])
                        {
                            intersectionWaypoints[j].inIntersection = true;
                            intersectionWaypoints[j].stop = true;
                        }
                        else
                        {
                            intersectionWaypoints.RemoveAt(j);
                        }
                    }
                }

                List<PedestrianWaypointSettings> directionWaypoints = allIntersectionComponents[i].GetDirectionWaypoints();
                if (directionWaypoints != null)
                {
                    for (int j = directionWaypoints.Count - 1; j >= 0; j--)
                    {
                        if (directionWaypoints[j])
                        {
                            directionWaypoints[j].crossing = true;
                        }
                        else
                        {
                            directionWaypoints.RemoveAt(j);
                        }
                    }
                }

                EditorUtility.SetDirty(allIntersectionComponents[i]);



            }


            for (int i = allPedestrianWaypoints.Count - 1; i >= 0; i--)
            {
                if (allPedestrianWaypoints[i].allowedPedestrians.Count != 0)
                {
                    allPedestrianEditorWaypoints.Add(allPedestrianWaypoints[i]);
                    GridCell cell = currentSceneData.GetCell(allPedestrianWaypoints[i].transform.position);
                    cell.AddPedestrianWaypoint(allPedestrianEditorWaypoints.Count - 1, allPedestrianWaypoints[i].name, allPedestrianWaypoints[i].allowedPedestrians.Cast<int>().ToList(), allPedestrianWaypoints[i].inIntersection);

                }
            }
            currentSceneData.allPedestrianWaypoints = allPedestrianEditorWaypoints.ToPlayWaypoints(allPedestrianEditorWaypoints).ToArray();
            for (int i = 0; i < allIntersectionComponents.Count; i++)
            {
                allIntersectionComponents[i].ConvertWaypoints(allPedestrianEditorWaypoints);
            }

            EditorUtility.SetDirty(currentSceneData);
            Debug.Log("Done assign pedestrian waypoints in " + (System.DateTime.Now - startTime));
        }

        private static void ClearPedestrianAllWaypoints(CurrentSceneData currentSceneData)
        {
            if (currentSceneData.grid != null)
            {
                for (int i = 0; i < currentSceneData.grid.Length; i++)
                {
                    for (int j = 0; j < currentSceneData.grid[i].row.Length; j++)
                    {
                        currentSceneData.grid[i].row[j].ClearPedestrianReferences();
                    }
                }
            }
        }
#if GLEY_TRAFFIC_SYSTEM
#if GLEY_PEDESTRIAN_SYSTEM
        static void GetPedestrianWaypoints(TrafficLightsIntersection intersection, TrafficLightsIntersectionSettings currentIntersection)
        {
            intersection.AddPedestrianWaypoints(currentIntersection.pedestrianWaypoints.ToListIndex(allPedestrianEditorWaypoints), currentIntersection.directionWaypoints.ToListIndex(allPedestrianEditorWaypoints), currentIntersection.redLightObjects, currentIntersection.greenLightObjects, currentIntersection.pedestrianGreenLightTime);
        }

        static void GetPedestrianWaypoints(PriorityIntersection intersection, PriorityIntersectionSettings currentIntersection)
        {
            for (int i = 0; i < currentIntersection.enterWaypoints.Count; i++)
            {
                intersection.AddPedestrianWaypoints(i, currentIntersection.enterWaypoints[i].pedestrianWaypoints.ToListIndex(allPedestrianEditorWaypoints));
                intersection.AddDirectionWaypoints(i, currentIntersection.enterWaypoints[i].directionWaypoints.ToListIndex(allPedestrianEditorWaypoints));
            }
        }
#endif
#endif
#endif
        #endregion

    }
}
