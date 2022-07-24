using System;
using System.Collections.Generic;
using System.Linq;
using mattatz.MeshSmoothingSystem;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float height = 5;
    [SerializeField] private int radialSegments = 20;
    [SerializeField] private Material material;
    [SerializeField] private float radius = 2;
    [SerializeField] [Range(0f, 1f)] private float hcAlpha = 0.5f;
    [SerializeField] [Range(0f, 1f)] private float hcBeta = 0.5f;


    public void SpriteToMesh()
    {
        GameObject go = new GameObject("Generated Level");
        Mesh mesh = new Mesh();
        CreateCylinder(mesh);
        var filter = go.AddComponent<MeshFilter>();
        var rnd = go.AddComponent<MeshRenderer>();

        MeshSmoothing.HCFilter(mesh, 10, hcAlpha, hcBeta);
        // MeshSmoothing.LaplacianFilter(mesh, 5);
        filter.mesh = mesh;
        rnd.material = material;
    }

    public void CreateCylinder(Mesh mesh)
    {
        var sprite = spriteRenderer.sprite;
        // mesh.SetVertices(Array.ConvertAll(sprite.vertices, i => (Vector3)i).ToList());
        // mesh.SetUVs(0, sprite.uv.ToList());
        // mesh.SetTriangles(Array.ConvertAll(sprite.triangles, i => (int)i), 0);

        mesh.name = "Cylinder";
        mesh.Clear();

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<List<int>> verticesLists = new List<List<int>>();

        for (int y = 0; y <= sprite.vertices.Length; y++)
        {
            List<int> verticesRow = new List<int>();
            for (int x = 0; x <= radialSegments; x++)
            {
                float u = (float)x / (float)radialSegments;

                var vertex = new Vector3();

                var spriteVert = sprite.vertices[Mathf.Max(0, y - 1)];
                var xOffset = spriteVert.x * height;

                vertex.x = xOffset + radius * Mathf.Sin(u * Mathf.PI * 2.0f);
                vertex.y = spriteVert.y * height + (spriteVert.y * height * 0.5f);
                vertex.z = radius * Mathf.Cos(u * Mathf.PI * 2.0f);

                vertices.Add(vertex);

                verticesRow.Add(vertices.Count - 1);
            }

            verticesLists.Add(verticesRow);
        }


        for (int x = 0; x < radialSegments; x++)
        {
            for (int y = 0; y < sprite.vertices.Length; y++)
            {
                var v1 = verticesLists[y][x];
                var v2 = verticesLists[y + 1][x];
                var v3 = verticesLists[y + 1][x + 1];
                var v4 = verticesLists[y][x + 1];

                triangles.Add(v1);
                triangles.Add(v2);
                triangles.Add(v4);

                triangles.Add(v2);
                triangles.Add(v3);
                triangles.Add(v4);
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        mesh.RecalculateNormals();
    }
}