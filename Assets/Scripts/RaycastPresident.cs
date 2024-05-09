using UnityEngine;

public sealed class RaycastPresident : MonoBehaviour
{
    [SerializeField] private float raycastDistance = 2.5f;

    private Camera _camera;

    public RaycastHit Hit
    {
        get;
        private set;
    }

    public GameObject DetectGameObject
    {
        get;
        private set;
    }

    public bool isRaycast
    {
        get;
        private set;
    }

    private LayerMask _layerMask;

    private void Start()
    {
        _camera = Camera.main;
        _layerMask = LayerMask.GetMask("Raycast");
    }

    private void Update()
    {
        isRaycast = Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hit, raycastDistance, _layerMask);
        Hit = hit;
        if (isRaycast) DetectGameObject = Hit.collider.gameObject;
    }
}
