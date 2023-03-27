using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour {

	public Renderer textureRenders;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

	public void DrawTexture(Texture2D texture) {		

		textureRenders.sharedMaterial.mainTexture = texture;
		textureRenders.transform.localScale = new Vector3 (texture.width, 1, texture.height);
	}

    public void DrawMesh(MeshData meshData, Texture2D texture){
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
	
}