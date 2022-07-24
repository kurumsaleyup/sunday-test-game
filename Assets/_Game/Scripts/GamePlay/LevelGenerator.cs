using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float height = 5;
    [SerializeField] private int radialSegments = 20;
    [SerializeField] private int heightSegments = 20;
    [SerializeField] private Material material;
    [SerializeField] private float radius = 2;


    public void SpriteToMesh()
    {
        GameObject go = new GameObject("Generated Level");
        Mesh mesh = new Mesh();
        CreateCylinder(mesh, radius, height, radialSegments, heightSegments);
        go.AddComponent<MeshFilter>().mesh = mesh;
        var rnd = go.AddComponent<MeshRenderer>();
        rnd.material = material;
    }

    public void CreateCylinder(Mesh mesh, float radius, float height,
        int radialSegments, int heightSegments)
    {
        var sprite = spriteRenderer.sprite;
        // mesh.SetVertices(Array.ConvertAll(sprite.vertices, i => (Vector3)i).ToList());
        // mesh.SetUVs(0, sprite.uv.ToList());
        // mesh.SetTriangles(Array.ConvertAll(sprite.triangles, i => (int)i), 0);

        mesh.name = "Cylinder";
        mesh.Clear();

        List<Vector2> uvs = new List<Vector2>();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<List<int>> verticesLists = new List<List<int>>();
        List<List<Vector2>> uvsLists = new List<List<Vector2>>();

        for (int y = 0; y <= heightSegments; y++)
        {
            List<int> verticesRow = new List<int>();
            List<Vector2> uvsRow = new List<Vector2>();

            var v = y / (float)heightSegments;

            for (int x = 0; x <= radialSegments; x++)
            {
                float u = (float)x / (float)radialSegments;

                var vertex = new Vector3();

                vertex.x = radius * Mathf.Sin(u * Mathf.PI * 2.0f);
                vertex.y = -v * height + (height * 0.5f);
                vertex.z = radius * Mathf.Cos(u * Mathf.PI * 2.0f);

                vertices.Add(vertex);
                uvs.Add(new Vector2(1 - u, 1 - v));

                verticesRow.Add(vertices.Count - 1);
                uvsRow.Add(new Vector2(u, 1 - v));
            }

            verticesLists.Add(verticesRow);
            uvsLists.Add(uvsRow);
        }


        for (int x = 0; x < radialSegments; x++)
        {
            for (int y = 0; y < heightSegments; y++)
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
        mesh.uv = uvs.ToArray();
        mesh.triangles = triangles.ToArray().Reverse().ToArray();

        mesh.RecalculateNormals();
    }
}