using System;
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
    [SerializeField] private List<LightPoint> _ligthPoints;
    [SerializeField] private LayerMask _canPlaceLightPointsOn;
    
    [Header("Snapping")]
    [SerializeField] private LayerMask _lightPointsMask;
    [SerializeField] private float _snappingDistance = 0.1f;
    
    [Header("Rendering")]
    private LineRenderer _lineRenderer;

    [Header("OtherCompontents")] 
    private EnergySystem _energySystem;
    
    //When create mesh? First replace list and create linked list,
    //then after adding point check if there is a loop inside linked list,
    //if is create mesh from points in linked list :))

    private void Awake()
    {
        _energySystem = GameObject.FindGameObjectWithTag("EnergySystem").GetComponent<EnergySystem>();
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
            LightPoint point = GetClosestPointToSnapIfExists();

            if (point != null)
            {
                //TO DO: Changing list to linked list and creating damaging mesh on vertices where is loop
                //and add some kind of visual representation before snap
                Debug.Log("Snap");
            }
            else
            {
                point = Instantiate(
                    _lightPointPrefab,
                    Pointer.OnScreenWorldPosition,
                    Quaternion.identity,
                    _lightPointsParent
                );

                _ligthPoints.Add(point);
            }

            _lineRenderer.AddNewPoint(point.transform.position);
            _energySystem.EnergyContainer.DecreaseEnergy(_lightAttackEnergyCost);
        }
    }

    private bool CanPlaceLightAttackPoint()
    {
        GameObject hoveredObject = Pointer.Instance.GetHoveredGameObject();
        return _canPlaceLightPointsOn.IsContainingLayer(hoveredObject.layer)
            && _energySystem.EnergyContainer.IsHavingEnergy(_lightAttackEnergyCost);
    }

    private LightPoint GetClosestPointToSnapIfExists()
    {
        LightPoint lightPoint = null;
        
        Collider[] colliders = Physics.OverlapSphere(Pointer.OnScreenWorldPosition,
            _snappingDistance,
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
}
