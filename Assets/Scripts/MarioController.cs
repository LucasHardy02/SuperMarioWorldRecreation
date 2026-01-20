using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class MarioController : MonoBehaviour
{
    public Rigidbody2D _marioRB;
    public Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    private Vector2 _moveInput;
    private Vector2 _jumpInput;

    private bool _isRunning;
    private bool _isGrounded;
    

    [Header("Movement")]
    public float _walkSpeed = 6f;
    public float _runSpeed = 10f;
    public float _acceleration = 60f;
    public float _deceleration = 80f;
    public float _airAcceleration = 30f;
    public float _airDeceleration = 10f;


    [Header("Jumping Values")]
    public float _gravityScale = 4;
    public float _jumpForce = 14;
    private float _rayLength = 0.1f;
    private bool _jumpRequested;
    public float _gravityMultiplier = 2.5f;
    public float _lowJumpGravityMultiplier = 2f;

    [Header("Forgiveness")]
    public float _coyoteTime = 0.1f;
    public float _jumpBufferTime = 0.1f;
    private float _timeSinceJumpPressed;
    private float _timeSinceLeftGround;


    [Header("Limits")]
    public float _maxFallSpeed = -25f;
    //Input
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void MarioJump()
    {
        if ((_isGrounded || _timeSinceLeftGround < _coyoteTime) && _timeSinceJumpPressed < _jumpBufferTime)
        {
            _marioRB.AddForceY(_jumpForce, ForceMode2D.Impulse);
            _timeSinceJumpPressed = 0;
            _timeSinceLeftGround = 0;

        }
       
    }

    //Input
    private void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpRequested = true;
            _timeSinceJumpPressed = 0;
        }

        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _isRunning = true;
            Debug.Log("_isRunning is true");

        }
        else
        {
            _isRunning = false;
        }
    }

    

    private void Update()
    {
        InputHandler();
    }
    private void FixedUpdate()
    {
        if (_jumpRequested)
        {
            _timeSinceJumpPressed += Time.fixedDeltaTime;

        }
        else if (_isGrounded)
        {
            _timeSinceLeftGround = 0;
        }
        else
        { 
            _timeSinceLeftGround += Time.fixedDeltaTime;
        }
        
            float _speedType = _isRunning ? _runSpeed : _walkSpeed;

        _marioRB.linearVelocity = new Vector2(_moveInput.x * _speedType, _marioRB.linearVelocity.y);


        if (_jumpRequested == true)
        {
            MarioJump();
            _jumpRequested = false;
        }

        RaycastHit2D hit = Physics2D.Raycast(_groundCheck.position, Vector2.down, _rayLength, _groundLayer);
        _isGrounded = hit.collider != null;

        Debug.DrawRay(_groundCheck.position, Vector2.down * _rayLength, Color.red);
    }
}
