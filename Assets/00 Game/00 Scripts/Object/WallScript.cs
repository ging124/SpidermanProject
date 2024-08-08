using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[ExecuteInEditMode]
public class WallScript : MonoBehaviour
{
    public MeshCollider meshCollider;

    public float highDetect = 0.1f;
    public List<Vector3> verticesList = new List<Vector3>();

    public float debugPointRadius = 0.18f;
    public int debugTextSize = 24;
    public int debugTextHeight = 3;
    public int debugLinethickness = 3;

    public float debugPointHeight = 1;

    /*public GameObject testPrefab;
    public List<GameObject> lists = new List<GameObject>();*/

    [ContextMenu("GetVertices")]
    private void GetVertices()
    {
        verticesList.Clear();

        Vector3[] vertices = meshCollider.sharedMesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            var vertexTransform = transform.TransformPoint(vertices[i]);

            int flag = 1;

            Vector3 direction1 = -Vector3.up + Vector3.right;

            if (Physics.Raycast(vertexTransform + Vector3.up * debugPointHeight, direction1, 2))
            {
                flag++;
            }

            Vector3 direction2 = -Vector3.up + Vector3.forward;

            if (Physics.Raycast(vertexTransform + Vector3.up * debugPointHeight, direction2, 2))
            {
                flag++;
            }

            Vector3 direction3 = -Vector3.up - Vector3.right;

            if (Physics.Raycast(vertexTransform + Vector3.up * debugPointHeight, direction3, 2))
            {
                flag++;
            }

            Vector3 direction4 = -Vector3.up - Vector3.forward;

            if (Physics.Raycast(vertexTransform + Vector3.up * debugPointHeight, direction4, 2))
            {
                flag++;
            }

            Vector3 direction5 = -Vector3.up;

            if (Physics.Raycast(vertexTransform + Vector3.up * debugPointHeight, direction5, 2))
            {
                flag++;
            }

            //RaycastHit hit;
            if (!verticesList.Contains(vertexTransform) && vertices[i].y > highDetect
                && flag == 4)
            {
                //Debug.Log(hit.collider.name);
                verticesList.Add(vertexTransform);
                //lists.Add(Instantiate(testPrefab, vertexTransform, Quaternion.identity, transform));
            }
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

        foreach (var verticies in verticesList)
        {
            Handles.Label(verticies + Vector3.up * debugTextHeight, index.ToString(), style);
            Gizmos.DrawSphere(verticies, debugPointRadius);
            index++;
        }

        Gizmos.color = Color.red;

        for (int i = 0; i < verticesList.Count; i++)
        {
            if (i == verticesList.Count - 1)
            {
                Handles.DrawLine(verticesList[i], verticesList[0], debugLinethickness);
            }
            else
            {
                Handles.DrawLine(verticesList[i], verticesList[i + 1], debugLinethickness);
            }

            Vector3 direction1 = - Vector3.up + Vector3.right;

            DebugRayDetection(verticesList[i], direction1);

            Vector3 direction2 = - Vector3.up + Vector3.forward;

            DebugRayDetection(verticesList[i], direction2);

            Vector3 direction3 = -Vector3.up - Vector3.right;

            DebugRayDetection(verticesList[i], direction3);

            Vector3 direction4 = -Vector3.up - Vector3.forward;

            DebugRayDetection(verticesList[i], direction4);

            Vector3 direction6 = -Vector3.up;

            DebugRayDetection(verticesList[i], direction6);
        }
    }

    void DebugRayDetection(Vector3 point, Vector3 direction)
    {
        Vector3 direction1 = direction;

        if (Physics.Raycast(point + Vector3.up * debugPointHeight, direction1, 2))
        {
            Debug.DrawRay(point + Vector3.up * debugPointHeight, direction1, Color.red);
        }
        else
        {
            Debug.DrawRay(point + Vector3.up * debugPointHeight, direction1, Color.green);
        }
    }
}
