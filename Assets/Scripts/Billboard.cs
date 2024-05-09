using UnityEngine;

public sealed class Billboard : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        Vector3 lookDirection = -_camera.transform.position + transform.position;
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}