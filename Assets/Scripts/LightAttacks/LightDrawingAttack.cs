using Energy;
using Inputs;
using MyUtils;
using System.Collections.Generic;
using UnityEngine;

namespace LightAttacks
{
    [RequireComponent(typeof(LineRenderer))]
    public class LightDrawingAttack : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private float _lightAttackEnergyCost = 15;
        [SerializeField] private float _lightAttackDamage = 10;

        [Header("Lighting Points")]
        [SerializeField] private Transform _lightPointsParent;
        [SerializeField] private LightPoint _lightPointPrefab;
        [SerializeField] private LayerMask _canPlaceLightPointsOn;
        [SerializeField] private float _maxDistanceToConnectPoints;
        private List<ILightConnectable> _attackPoints;

        [Header("Snapping")]
        [SerializeField] private LayerMask _lightPointsMask;
        [SerializeField] private float _snappingDistance = 0.1f;

        [Header("Rendering")]
        [SerializeField] private Material _lightAttackMaterial;
        private LineRenderer _lineRenderer;

        [Header("OtherCompontents")]
        private EnergySystem _energySystem;

        private void Awake()
        {
            _energySystem = GameObject.FindGameObjectWithTag("EnergySystem").GetComponent<EnergySystem>();
            _attackPoints = new List<ILightConnectable>();
        }

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            PlayerInputHandler.Instance.AddFunctionToOnPointerClick(HandleLightAttack);
        }

        private void HandleLightAttack()
        {
            if (CanPlaceLightAttackPoint())
            {
                ILightConnectable snapPoint = GetLightPointInRadiusIfExists(_snappingDistance);

                if (snapPoint is not null && snapPoint.CanConnect)
                {
                    //TODO: Changing list to linked list and creating damaging mesh on vertices where is loop
                    //and add some kind of visual representation before snap
                    ConnectToLastAttackPoint(snapPoint);
                    _lineRenderer.AddNewPoint(snapPoint.CurrentPosition);

                    ILightConnectable[] connectedPoints = GetAttackPointsConnectedInLoop();
                    if (connectedPoints is not null)
                    {
                        GameObject gameObject = new GameObject("LightAttackArea");
                        var meshFilter = gameObject.AddComponent<MeshFilter>();
                        var meshRenderer = gameObject.AddComponent<MeshRenderer>();

                        Mesh mesh = new Mesh();
                        Vector3[] vertices = new Vector3[connectedPoints.Length];
                        int[] triangles = new int[3 * (connectedPoints.Length - 2)];

                        for (int i = 0; i < connectedPoints.Length; i++)
                        {
                            vertices[i] = connectedPoints[i].CurrentPosition;
                            connectedPoints[i].CanConnect = false;
                        }

                        int core = 0, current = 0;
                        for (int i = 0; i < triangles.Length; i++)
                        {
                            if (i > 1 && i % 3 == 0)
                            {
                                triangles[triangles.Length - 1 - i] = core;
                                current--;
                            }
                            else triangles[triangles.Length - 1 - i] = current++;
                        }

                        Debug.Log(string.Join(" ", triangles));

                        mesh.vertices = vertices;
                        mesh.triangles = triangles;
                        meshRenderer.material = _lightAttackMaterial;
                        mesh.RecalculateNormals();
                        meshFilter.mesh = mesh;

                        _lineRenderer.positionCount = 0;
                        _attackPoints.Clear();
                    }
                }
                else
                {
                    ILightConnectable pointToConnectLine = GetLightPointInRadiusIfExists(_maxDistanceToConnectPoints);
                    ILightConnectable newPoint = Instantiate(
                        _lightPointPrefab,
                        Pointer.OnScreenWorldPosition,
                        Quaternion.identity,
                        _lightPointsParent
                    );

                    if (pointToConnectLine is null)
                    {
                        _lineRenderer.positionCount = 0;
                        _attackPoints.Clear();
                    }

                    ConnectToLastAttackPoint(newPoint);

                    _attackPoints.Add(newPoint);
                    _lineRenderer.AddNewPoint(newPoint.CurrentPosition);
                }

                _energySystem.EnergyContainer.DecreaseEnergy(_lightAttackEnergyCost);
            }
        }

        private ILightConnectable[] GetAttackPointsConnectedInLoop()
        {
            if (_attackPoints.Count < 3) return null;

            var result = new List<ILightConnectable>();
            ILightConnectable slow = _attackPoints[0], fast = _attackPoints[0].IsConnectedTo;
            while (fast is not null && fast.IsConnectedTo is not null)
            {
                if (slow == fast)
                {
                    do
                    {
                        result.Add(fast);
                        fast = fast.IsConnectedTo;
                    } while (slow != fast);

                    break;
                }

                slow = slow.IsConnectedTo;
                fast = fast.IsConnectedTo.IsConnectedTo;
            }

            return result.ToArray();
        }

        private bool CanPlaceLightAttackPoint()
        {
            GameObject hoveredObject = Pointer.Instance.GetHoveredGameObject();
            return _canPlaceLightPointsOn.IsContainingLayer(hoveredObject.layer)
                && _energySystem.EnergyContainer.IsHavingEnergy(_lightAttackEnergyCost);
        }

        private ILightConnectable GetLightPointInRadiusIfExists(float radius)
        {
            ILightConnectable lightPoint = null;

            Collider[] colliders = Physics.OverlapSphere(Pointer.OnScreenWorldPosition,
                radius,
                _lightPointsMask,
                QueryTriggerInteraction.UseGlobal
            );

            if (colliders.Length > 0)
            {
                Collider closestCollider = null;
                float minDistance = float.MaxValue;
                foreach (var col in colliders)
                {
                    float distance = Vector3.Distance(Pointer.OnScreenWorldPosition, col.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestCollider = col;
                    }
                }

                closestCollider.TryGetComponent(out lightPoint);
            }

            return lightPoint;
        }

        private void ConnectToLastAttackPoint(ILightConnectable newPoint)
        {
            if (_attackPoints.Count > 0)
                _attackPoints[^1].IsConnectedTo = newPoint;
        }
    }
}