using UnityEngine;

public sealed class PlayerMovement : MonoBehaviour
{
    // Будь ласка, юзайте патерн solid :)
        // га ? Що таке той ваш солід ?
    private CharacterController _characterController;
    private Vector3 _moveDirection = Vector3.zero;

    [SerializeField] private Camera cameraPlayer;
    [SerializeField] private float mouseSensitivity = 25f;
    [SerializeField] private bool cameraLock;

    [SerializeField] private float playerSpeedMovement = 25f;
    [SerializeField] private float playerJumpForce = 20f;

    [SerializeField] private float modifiedGravity = 2f;

    private float _mouseX;
    private float _mouseY;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _characterController.height = 1f;
            playerSpeedMovement = 5f;
        }
        else
        {
            _characterController.height = 2.1f;
            playerSpeedMovement = 10f;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeedMovement = 15f;
        }
        else
        {
            playerSpeedMovement = 10f;
        }


        Movement();
        _characterController.Move(_moveDirection * Time.deltaTime);

        
    }

    private void FixedUpdate()
    {
        Gravity();
    }

    private void LateUpdate()
    {
        BodyRotate();
    }

    private void Movement()
    {
        _moveDirection = new Vector3(Input.GetAxis("Horizontal") * playerSpeedMovement, _moveDirection.y, Input.GetAxis("Vertical") * playerSpeedMovement);
        _moveDirection = transform.TransformDirection(_moveDirection);
        if (!_characterController.isGrounded) return;
        _moveDirection.y = Input.GetAxis("Jump") * playerJumpForce;
    }

    private void Gravity()
    {
        _moveDirection.y -= -Physics.gravity.y * modifiedGravity * Time.deltaTime;
    }

    private void BodyRotate()
    {
        _mouseX += Input.GetAxis("Mouse X") * mouseSensitivity;
        _mouseY += Input.GetAxis("Mouse Y") * mouseSensitivity;

        _mouseY = Mathf.Clamp(_mouseY, -90, 90);

        Quaternion cameraRotate = Quaternion.Euler(-_mouseY, _mouseX, 0);
        Quaternion bodyRotate = Quaternion.Euler(0, _mouseX, 0);

        if (cameraLock || !Application.isFocused) return;
        transform.rotation = bodyRotate;
        cameraPlayer.transform.rotation = cameraRotate;
    }
}
