using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField] private float worldHeightOffset = 0;
    public static Vector3 OnScreenWorldPosition { get; private set; }
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        UpdateMousePosition();
    }

    private void UpdateMousePosition()
    {
        OnScreenWorldPosition = _mainCamera.ScreenToWorldPoint(PlayerInputHandler.Instance.LookInput);
        OnScreenWorldPosition = new Vector3(
            OnScreenWorldPosition.x,
            worldHeightOffset,
            OnScreenWorldPosition.z
        );
    }
}
