using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[ExecuteInEditMode]
public class WallScript : MonoBehaviour
{
    public MeshCollider meshCollider;
    public float highDetect = 0.1f;
    public List<Vector3> verticesList = new List<Vector3>();

    public float debugPointRadius;
    public int debugTextSize;


    /*public GameObject testPrefab;
    public List<GameObject> lists = new List<GameObject>();*/

    [ContextMenu("GetVertices")]
    private void GetVertices()
    {
        verticesList.Clear();

        Vector3[] vertices = meshCollider.sharedMesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            var vertexTransform = vertices[i];
            vertexTransform.Scale(this.transform.lossyScale);
            vertexTransform = this.transform.rotation * vertexTransform;
            vertexTransform += this.transform.position;

            if (!verticesList.Contains(vertexTransform) && vertices[i].y > highDetect)
            {
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

    private void OnDrawGizmos()
    {
        
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
            Handles.Label(verticies + Vector3.up * 0.5f, index.ToString(), style);
            Gizmos.DrawSphere(verticies, debugPointRadius);
            index++;
        }

        Gizmos.color = Color.red;

        for (int i = 0; i < verticesList.Count; i++)
        {
            if (i == verticesList.Count - 1)
            {
                Gizmos.DrawLine(verticesList[i], verticesList[0]);
            }
            else
            {
                Gizmos.DrawLine(verticesList[i], verticesList[i + 1]);
            }
        }
    }
}
