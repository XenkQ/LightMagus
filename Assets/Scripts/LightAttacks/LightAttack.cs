using System.Collections.Generic;
using MyUtils;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightAttack : MonoBehaviour
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

            if (snapPoint is not null)
            {
                //TO DO: Changing list to linked list and creating damaging mesh on vertices where is loop
                //and add some kind of visual representation before snap
                Debug.Log("Snap");
                ConnectToLastAttackPoint(snapPoint);
                _lineRenderer.AddNewPoint(snapPoint.CurrentPosition);
            }
            else
            {
                ILightConnectable pointToConnectLine = GetLightPointInRadiusIfExists(_maxDistanceToConnectPoints);
                ILightConnectable newPoint= Instantiate(
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
