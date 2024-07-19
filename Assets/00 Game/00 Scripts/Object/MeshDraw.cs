using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshDraw : MonoBehaviour
{
    public MeshCollider meshCollider;
    public GameObject pointObject;
    public List<Vector3> pointList = new List<Vector3>();

    private void Start()
    {
        for (int i = 0; i < meshCollider.sharedMesh.vertices.Length; i++)
        {
            Debug.Log(meshCollider.sharedMesh.vertices[i]);
            var vertexTransform = meshCollider.sharedMesh.vertices[i] * this.transform.lossyScale.x + this.transform.position;

            if (meshCollider.sharedMesh.vertices[i].x != 0 && meshCollider.sharedMesh.vertices[i].y != 0 && meshCollider.sharedMesh.vertices[i].z != 0 && meshCollider.sharedMesh.vertices[i].y == meshCollider.sharedMesh.vertices[i - 1].y) return;

            if (!pointList.Contains(vertexTransform) && meshCollider.sharedMesh.vertices[i].y != 0)
            {
                pointList.Add(Instantiate(pointObject, vertexTransform, Quaternion.identity).transform.position);
            }
        }
    }
}
