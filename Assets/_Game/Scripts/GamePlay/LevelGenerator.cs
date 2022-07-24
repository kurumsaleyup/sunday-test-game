using System;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField] private SpriteRenderer spriteRenderer;
    
    
    public void SpriteToMesh()
    {
        Mesh mesh = new Mesh();
        GameObject go = new GameObject();
        go.AddComponent<MeshFilter>().mesh = mesh;
        go.AddComponent<MeshRenderer>();

        var sprite = spriteRenderer.sprite;
        mesh.SetVertices(Array.ConvertAll(sprite.vertices, i => (Vector3)i).ToList());
        mesh.SetUVs(0,sprite.uv.ToList());
        mesh.SetTriangles(Array.ConvertAll(sprite.triangles, i => (int)i),0);

    }
}