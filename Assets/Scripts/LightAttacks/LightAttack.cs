using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(LineRenderer))]
public class LightAttack : MonoBehaviour
{
    [SerializeField] private Transform _lightPointsParent;
    [SerializeField] private LightPoint _lightPointPrefab;
    [SerializeField] private List<LightPoint> _ligthPoints;
    private LineRenderer _lineRenderer;
    
    [SerializeField] private LayerMask _lightPointsMask;
    [SerializeField] private float _snappingDistance = 0.1f;
    
    //When create mesh? First replace list and create linked list,
    //then after adding point check if there is a loop inside linked list,
    //if is create mesh from points in linked list :))

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LightPoint point = GetClosestPointToSnapIfExists();

            if (point != null)
            {
                //TO DO: Changing list to linked list and creating damaging mesh on vertices where is loop
                //and add some kind of visual representation before snap
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
            
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, point.transform.position);
        }
    }

    private LightPoint GetClosestPointToSnapIfExists()
    {
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
            
            return closestCollider.GetComponent<LightPoint>();
        }

        return null;
    }
}
