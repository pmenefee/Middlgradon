using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Attach to GameObject to spawn the world on game start.
/// </summary>
[RequireComponent(typeof(MeshFilter))]
public class SpawnWorld : MonoBehaviour
{
    public int width;
    public int height;

    Mesh mesh;

    List<Vector3> verticies;
    int[] triangles;
    int[] terrainData;

    public void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    private void CreateShape()
    {
        verticies = new List<Vector3>();
        for (int x = 0; x <= 12; x++)
        {
            for (int z = 0; z <= 12; z++)
            {
                verticies.Add(new Vector3(x, 0, z));
            }
        }
        // 0,0,0
        // 0,0,1
        // 1,0,0
        // 1,0,1

        //verticies.Add(new Vector3(0, 0, 0));
        //verticies.Add(new Vector3(0, 0, 1));
        //verticies.Add(new Vector3(1, 0, 0));
        //verticies.Add(new Vector3(1, 0, 1));

        triangles = new int[]
        {
            0,1,2,
            1,3,2
        };
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = verticies.ToArray();
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
