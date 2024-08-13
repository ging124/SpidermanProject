using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class WallScript : MonoBehaviour
{
    public RaySearch raySearch;
    public MeshCollider meshCollider;

    public float highDetect = 0.1f;
    public float distanceVertices = 1.2f;

    public List<Vector3> verticesList = new List<Vector3>();

    public List<List<Vector3>> groupedVectors;

    public List<Vector3> point;


    public float debugPointRadius = 0.18f;
    public int debugTextSize = 24;
    public int debugTextHeight = 3;

    [ContextMenu("GetVertices")]
    private void GetVertices()
    {
        verticesList.Clear();
        point.Clear();
        raySearch.meshPoints.Clear();
        raySearch.cornerPoints.Clear();

        Vector3[] vertices = meshCollider.sharedMesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertexTransform = transform.TransformPoint(vertices[i]);

            if (!verticesList.Contains(vertexTransform) && vertexTransform.y > highDetect)
            {
                verticesList.Add(vertexTransform);
            }

        }

        groupedVectors = new List<List<Vector3>>(SplitByHeight(verticesList));

        for (int i = 0; i < groupedVectors.Count; i++)
        {
            float maxDistance = float.MinValue;
            int flag = 0;
            for (int j = 0; j < groupedVectors[i].Count; j++)
            {
                float distance = Vector3.Distance(groupedVectors[i][j], this.transform.position);
                if (maxDistance < distance)
                {
                    maxDistance = distance;
                    flag = j;
                }
            }
            point.Add(groupedVectors[i][flag]);
        }


        foreach(var point in point)
        {
            Vector3 wallTransform = this.transform.position;
            wallTransform.y = point.y;
            Vector3 direction = (point - wallTransform).normalized + point;
            direction.x = point.x;
            if (Physics.Raycast(direction, (point - direction).normalized, out RaycastHit hit))
                raySearch.FindNext(hit.point, hit.normal);
        }

    }

    public Vector3 GetZipPoint(Vector3 point)
    {
        if (verticesList.Count == 1) return verticesList[0];
        if (verticesList.Count == 7) verticesList.Remove(verticesList[6]);

        Vector3 minPoint = Vector3.zero;
        float minDistance = float.MaxValue;

        for (int i = 0; i < verticesList.Count; i++)
        {
            Vector3 projectedPoint;

            if (i == verticesList.Count - 1)
            {
                Debug.DrawLine(verticesList[i], verticesList[0], Color.blue);
                projectedPoint = ProjectPointOnLine(point, verticesList[i], verticesList[0]);
            }
            else
            {
                Debug.DrawLine(verticesList[i], verticesList[i + 1], Color.blue);
                projectedPoint = ProjectPointOnLine(point, verticesList[i], verticesList[i + 1]);
            }

            float disctance = (point - projectedPoint).sqrMagnitude;

            if (minDistance >= disctance)
            {
                minDistance = disctance;
                minPoint = projectedPoint;
            }
        }

        return minPoint;
    }

    public Vector3 ProjectPointOnLine(Vector3 p, Vector3 a, Vector3 b)
    {
        return Vector3.Project((p - a), (b - a)) + a;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.black;
        style.fontSize = debugTextSize;

        int index = 0;

        foreach (var verticies in point)
        {
            Handles.Label(verticies + Vector3.up * debugTextHeight, index.ToString(), style);
            Gizmos.DrawSphere(verticies, debugPointRadius);
            DebugRayDetection(verticies);
            index++;
        }
        Gizmos.color = Color.red;

    }

    void DebugRayDetection(Vector3 point)
    {
        Vector3 wallTransform = this.transform.position;
        wallTransform.y = point.y;
        Vector3 direction = (point - wallTransform).normalized + point;
        direction.x = point.x;

        if (Physics.Raycast(direction, (point - direction).normalized, 2))
        {
            Debug.DrawRay(direction, (point - direction).normalized, Color.red);
        }
        else
        {
            Debug.DrawRay(direction, (point - direction).normalized, Color.green);
        }
    }

    public List<List<Vector3>> SplitByHeight(List<Vector3> vectors)
    {
        // Tạo dictionary để nhóm các vector theo giá trị y
        SortedDictionary<float, List<Vector3>> groupedByHeight = new SortedDictionary<float, List<Vector3>>();

        foreach (Vector3 vec in vectors)
        {
            float height = vec.y;
            if (!groupedByHeight.ContainsKey(height))
            {
                groupedByHeight[height] = new List<Vector3>();
            }

            groupedByHeight[height].Add(vec);
        }

        Dictionary<float, List<Vector3>> final = new();
        var tmp = groupedByHeight.First();
        int index = 0;
        foreach (var item in groupedByHeight)
        {
            if(Mathf.Abs(tmp.Key - item.Key) < distanceVertices) {
                if (!final.ContainsKey(index))
                {
                    final[index] = new List<Vector3>();
                }
                final[index].AddRange(item.Value);
            }
            else
            {
                index++;
                if (!final.ContainsKey(index))
                {
                    final[index] = new List<Vector3>();
                }
                final[index].AddRange(item.Value);
            }
            tmp = item;
        }

        // Chuyển dictionary thành list các list
        return final.Values.ToList();
    }
}



