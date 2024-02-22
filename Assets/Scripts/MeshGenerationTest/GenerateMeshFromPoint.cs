using LightAttacks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateMeshFromPoint : MonoBehaviour
{
    [SerializeField] private int _pointsToCreate;
    [SerializeField] private float _randomPointsCircleRadius = 4f;
    [SerializeField] private LightPoint _pointPrefab;
    [SerializeField] private Material _lightMat;
    [SerializeField] private List<LightPoint> _currentPoints;
    [SerializeField] private bool _generateLogMessage = false;
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
        Vector3[] vertices = new Vector3[_currentPoints.Count];
        int[] triangles = new int[3 * (_currentPoints.Count - 2)];

        var currentVertices = new Dictionary<int, Vector3>();

        for (int i = 0; i < _currentPoints.Count; i++)
        {
            vertices[i] = _currentPoints[i].CurrentPosition;
            currentVertices.Add(i, vertices[i]);
            Instantiate(new GameObject(i.ToString()), _currentPoints[i].CurrentPosition, Quaternion.identity, transform);
            _currentPoints[i].CanConnect = false;
        }


        //Not working maybe learn how to use Polygon triangulation algorithm

        int minXVertexKey = currentVertices.OrderBy(x => x.Value.x).ThenByDescending(x => x.Value.z).Select(x => x.Key).First();
        var higherPoints = currentVertices.Where(x => x.Key != minXVertexKey && x.Value.z >= currentVertices[minXVertexKey].z).OrderBy(x => x.Value.x).ToDictionary(x => x.Key, x => x.Value);
        var lowerPoints = currentVertices.Where(x => x.Key != minXVertexKey && x.Value.z < currentVertices[minXVertexKey].z).OrderByDescending(x => x.Value.x).ToDictionary(x => x.Key, x => x.Value);
        triangles[0] = minXVertexKey;
        currentVertices.Remove(minXVertexKey);

        currentVertices = higherPoints.Concat(lowerPoints).ToDictionary(x => x.Key, x => x.Value);

        if (_generateLogMessage)
        {
            Debug.Log(string.Join("\n", currentVertices));
            Debug.Log("Vert: " + currentVertices.Count + " | | " + "Triang" + triangles.Length);
        }

        int core = 0, current = 1;
        foreach (var item in currentVertices)
        {
            triangles[current++] = item.Key;
        }

        Debug.Log(string.Join(" ", triangles));

        mesh.vertices = vertices;
        mesh.triangles = triangles;
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
        foreach(ISelfDestroyable point in _currentPoints) point.DestroySelf();
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