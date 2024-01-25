using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] private float worldHeightOffset = 0;
    public static Vector3 OnScreenWorldPosition { get; private set; }
    private Camera _mainCamera;
    
    public static event PlayerInteractions.InteractionDelegate OnPointerShortInteraction;
    public static event PlayerInteractions.InteractionDelegate OnPointerLongInteraction;

    private void Awake()
    {
        _mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        UpdateOnScreenWorldPosition();
    }
    
    private void Update()
    {
        if(PlayerInputHandler.Instance.IsPointerShortInteraction)
            OnPointerShortInteraction?.Invoke(GetHoveredGameObject());
        
        if(PlayerInputHandler.Instance.IsPointerLongInteraction)
            OnPointerLongInteraction?.Invoke(GetHoveredGameObject());
    }

    private GameObject GetHoveredGameObject()
    {
        RaycastHit hit;
        Ray ray = _mainCamera.ScreenPointToRay(PlayerInputHandler.Instance.LookInput);
        
        if (Physics.Raycast(ray, out hit))
            return hit.transform.gameObject;

        return null;
    }

    private void UpdateOnScreenWorldPosition()
    {
        OnScreenWorldPosition = _mainCamera.ScreenToWorldPoint(PlayerInputHandler.Instance.LookInput);
        OnScreenWorldPosition = new Vector3(
            OnScreenWorldPosition.x,
            worldHeightOffset,
            OnScreenWorldPosition.z
        );
    }
}
