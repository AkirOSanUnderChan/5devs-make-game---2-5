using UnityEngine;

public sealed class Zoom : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private float playerMaxZoom;
    [SerializeField] private float playerZoomSpeed;
    private float _cameraDefaultFov;
    private float _currentFov;

    private void Start()
    {
        _cameraDefaultFov = _camera.fieldOfView;
    }
    
    private void Update()
    {
        Zooming();
    }
    
    private void Zooming()
    {
        if (Input.GetButton("Zoom"))
            _currentFov -= playerZoomSpeed;
        else
            _currentFov += playerZoomSpeed+1;
        
        _currentFov = Mathf.Clamp(_currentFov, playerMaxZoom, _cameraDefaultFov);
        _camera.fieldOfView = _currentFov;
    }
}
