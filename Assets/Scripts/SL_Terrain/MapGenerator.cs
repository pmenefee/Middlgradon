using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

    public enum DrawMode {NoiseMap, ColorMap, Mesh};
    public DrawMode drawMode;


    const int mapChunkSize = 241;
    [Range(0,6)]
    public int levelOfDetail;
    public enum TextureMode {Bilinear, Point, Trilinear};
    public TextureMode textureMode;
	public float noiseScale;

	public int octaves;
	[Range(0,1)]
	public float persistance;
	public float lacunarity;

	public int seed;
	public Vector2 offset;

    public float meshHeightMultiplier;

    [Tooltip("Adjust slope to make some layer flatter or steeper than others.")] 
    public AnimationCurve meshHeightCurve;

	public bool autoUpdate;

    public TerrainType[] regions;

	public void GenerateMap() {
		float[,] noiseMap = Noise.GenerateNoiseMap (mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize]; // All of the colors
        for (int y = 0; y < mapChunkSize; y++){
            for (int x = 0; x<mapChunkSize; x++){
                float currentHeight = noiseMap[x,y];
                // Now check to see which height this region falls within
                for (int i=0; i< regions.Length; i++){
                    if(currentHeight <= regions[i].height){
                        colorMap[y * mapChunkSize + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        FilterMode filterMode = FilterMode.Bilinear;

        switch(textureMode){
            case TextureMode.Bilinear:
                filterMode = FilterMode.Bilinear;
                break;
            case TextureMode.Point:
                filterMode = FilterMode.Point;
                break;
            case TextureMode.Trilinear:
                filterMode = FilterMode.Trilinear;
                break;
        }

		MapDisplay display = FindObjectOfType<MapDisplay> ();
        if(drawMode == DrawMode.NoiseMap){
		    display.DrawTexture (TextureGenerator.TextureFromHeightMap(noiseMap, filterMode));
        } else if (drawMode == DrawMode.ColorMap){
            display.DrawTexture (TextureGenerator.TextureFromColourMap(colorMap, mapChunkSize, mapChunkSize, filterMode));
        } else if (drawMode == DrawMode.Mesh){
            display.DrawMesh (MeshGenerator.GenerateTerrainMesh (noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColourMap(colorMap, mapChunkSize, mapChunkSize, filterMode));
        }
	}


    // This is called automatically when one of the variables are changed in the instpector.
	void OnValidate() {
		if (lacunarity < 1) {
			lacunarity = 1;
		}
		if (octaves < 0) {
			octaves = 0;
		}
	}

}

[System.Serializable]
public struct TerrainType {
    public string name;
    public float height;
    public Color color;

}