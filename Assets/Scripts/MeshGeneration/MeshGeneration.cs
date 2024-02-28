using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TriangleNet.Geometry;
using TriangleNet.Meshing;
using UnityEngine;

public class MeshGeneration : MonoBehaviour
{
    public static Mesh GeneratePlainMeshFromPoints(Vector2[] points)
    {
        var options = new ConstraintOptions() { ConformingDelaunay = true };
        var quality = new QualityOptions() { MinimumAngle = 0 };

        Mesh mesh = new Mesh();
        var polygon = new Polygon();

        foreach (var item in points)
            polygon.Add(new Vertex(item.x, item.y));

        var triangleMesh = polygon.Triangulate(options, quality);
        var triangles = new int[triangleMesh.Triangles.Count * 3];
        var vertices = new List<Vector3>(triangleMesh.Vertices.Count);

        foreach (var vertex in triangleMesh.Vertices)
            vertices.Add(new Vector3((float)vertex.X, 0, (float)vertex.Y));

        int currentTriangle = 0;
        foreach (var triange in triangleMesh.Triangles)
            for (int i = 0; i < 3; i++)
                triangles[currentTriangle++] = triange.GetVertexID(i);

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.Reverse().ToArray();
        mesh.RecalculateNormals();

        return mesh;
    }
}
