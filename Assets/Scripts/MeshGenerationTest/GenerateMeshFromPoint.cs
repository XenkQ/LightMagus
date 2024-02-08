using LightAttacks;
using System.Collections.Generic;
using UnityEngine;

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
        Vector3[] vertices = new Vector3[_currentPoints.Count];
        int[] triangles = new int[3 * (_currentPoints.Count - 2)];

        for (int i = 0; i < _currentPoints.Count; i++)
        {
            vertices[i] = _currentPoints[i].CurrentPosition;
            _currentPoints[i].CanConnect = false;
        }

        int core = 0, current = 0;
        for (int i = 0; i < triangles.Length; i++)
        {
            if (i > 1 && i % 3 == 0)
            {
                triangles[i] = core;
                current--;
            }
            else triangles[i] = current++;
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
            Vector2 randomCirclePoint = Random.insideUnitCircle * _randomPointsCircleRadius;
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