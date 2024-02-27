using LightAttacks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TriangleNet.Geometry;
using TriangleNet.Meshing;

public class GenerateMeshFromPoint : MonoBehaviour
{
    [SerializeField] private int _pointsToCreate;
    [SerializeField] private float _randomPointsCircleRadius = 4f;
    [SerializeField] private LightPoint _pointPrefab;
    [SerializeField] private Material _lightMat;
    [SerializeField] private List<LightPoint> _currentPoints;
    private GameObject _currentMeshObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ClearAllPoints();
            _currentPoints.Clear();
            ClearMeshGameObject();
            GenerateRandomPoints(_pointsToCreate);
            GenerateMesh();
        }
    }

    private void GenerateMesh()
    {
        _currentMeshObject = new GameObject("LightAttackArea");
        var meshFilter = _currentMeshObject.AddComponent<MeshFilter>();
        var meshRenderer = _currentMeshObject.AddComponent<MeshRenderer>();


        Mesh mesh = new Mesh();
        var vertices = new List<Vector3>(_currentPoints.Count);

        for (int i = 0; i < _currentPoints.Count; i++)
        {
            vertices.Add(_currentPoints[i].CurrentPosition);
            Instantiate(new GameObject($"Point{i}"), _currentPoints[i].CurrentPosition, Quaternion.identity, transform);
            _currentPoints[i].CanConnect = false;
        }

        var polygon = new Polygon();
        foreach (var item in _currentPoints)
            polygon.Add(new Vertex(item.CurrentPosition.x, item.CurrentPosition.z));

        var options = new ConstraintOptions() { ConformingDelaunay = true };
        var quality = new QualityOptions() { MinimumAngle = 0 };

        var triangleMesh = polygon.Triangulate(options, quality);

        var triangles = new int[triangleMesh.Triangles.Count * 3];

        vertices = new List<Vector3>(triangleMesh.Vertices.Count);
        foreach (var vertex in triangleMesh.Vertices)
            vertices.Add(new Vector3((float)vertex.X, 0, (float)vertex.Y));

        int currentTriangle = 0;
        foreach (var triange in triangleMesh.Triangles)
            for (int i = 0; i < 3; i++)
                triangles[currentTriangle++] = triange.GetVertexID(i);

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.Reverse().ToArray();
        meshRenderer.material = _lightMat;
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }

    private void GenerateRandomPoints(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 randomCirclePoint = UnityEngine.Random.insideUnitCircle * _randomPointsCircleRadius;
            Vector3 pos = transform.position + new Vector3(randomCirclePoint.x, 0, randomCirclePoint.y);
            LightPoint point = Instantiate(_pointPrefab, pos, Quaternion.identity, transform);
            _currentPoints.Add(point);
        }
    }

    private void ClearAllPoints()
    {
        foreach (ISelfDestroyable point in _currentPoints) point.DestroySelf();
    }

    private void ClearMeshGameObject()
    {
        GameObject.Destroy(_currentMeshObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _randomPointsCircleRadius);
    }
}