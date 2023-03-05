using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateWorld : MonoBehaviour
{    
    [SerializeField]
    public Vector3 GridSize = new Vector3(100, 5, 100);  
    [SerializeField]
    public Material material = null;

    [ContextMenuItem("Randomize Terrain", "RandomizeTerrain")]
    [SerializeField]
    [Range(1,10)]
    public float zoom = 1f;
    [ContextMenuItem("Randomize Terrain", "RandomizeTerrain")]
    [SerializeField]
    [Range(.01f, 1f)]
    public float noiselimit = 0.3f;

    private Mesh mesh = null;
    private float old_noiseLimit = 0f;
    private float old_zoom = 0f;
    private Vector3 old_GridSize = new Vector3(100, 5, 100);


    // Start is called before the first frame update

    void Start()
    {        
        Refresh();
    }

    private void Update()
    {
        if(old_noiseLimit != noiselimit || old_zoom != zoom || old_GridSize != GridSize)
        {
            Refresh();
            old_noiseLimit = noiselimit;
            old_zoom = zoom;
            old_GridSize = GridSize;
        }
    }



    [ContextMenu("Refresh Terrain")]
    public void Refresh()
    {
        MakeGrid();
        Noise2d();
        Noise3d();
        March();
    }

    /// <summary>
    /// Will randomize the terrain noiselimit when triggered.
    /// </summary>
    [ContextMenu("Randomize Terrain")]
    private void RandomizeTerrain()
    {
        //zoom = UnityEngine.Random.Range(1f, 10f);
        noiselimit = UnityEngine.Random.Range(0f, 1f);
        Refresh();
    }

    private void MakeGrid()
    {
        MarchingCube.grd = new GridPoint[(int)GridSize.x, (int)GridSize.y, (int)GridSize.z];
        //Debug.Log((int)GridSize.x +","+ (int)GridSize.y + "," + (int)GridSize.z);
        for(int z = 0; z < GridSize.z; z++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    MarchingCube.grd[x, y, z] = new GridPoint();
                    MarchingCube.grd[x, y, z].Position = new Vector3(x, y, z);
                    MarchingCube.grd[x, y, z].On = false;
                }
            }
        }
    }

    private void Noise2d()
    {
        for (int z = 0; z < GridSize.z; z++)
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                float nx = (x / GridSize.x) * zoom;
                float nz = (z / GridSize.z) * zoom;
                float height = Mathf.PerlinNoise(nx, nz) * GridSize.y;

                for (int y = 0; y < GridSize.y; y++)
                {
                    MarchingCube.grd[x, y, z].On = (y < height);
                }
            }
        }
    }

    private void Noise3d()
    {
        for (int z = 0; z < GridSize.z; z++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    float nx = (x / GridSize.x) * zoom;
                    float ny = (y / GridSize.y) * zoom;
                    float nz = (z / GridSize.z) * zoom;
                    float noise = MarchingCube.PerlinNoise3D(nx, ny, nz);  //0..1

                    MarchingCube.grd[x, y, z].On = (noise > noiselimit);
                }
            }
        }
    }

    private void March()
    {
        GameObject go = this.gameObject;
        mesh = MarchingCube.GetMesh(ref go, ref material);

        MarchingCube.Clear();
        MarchingCube.MarchCubes();

        MarchingCube.SetMesh(ref mesh);
    }

}
