using UnityEngine;
using UnityEngine.InputSystem;

public class MarioController : MonoBehaviour
{
    public Rigidbody2D _marioRB;
    public Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    private Vector2 _moveInput;
    private Vector2 _jumpInput;

    private bool _isRunning;
    private bool _isGrounded;
    

    [Header("Walk/Run Values")]
    public float _walkSpeed;
    public float _runSpeed;

    [Header("Jumping Values")]
    public float _jumpHeight;
    private float _rayLength = 0.1f;

    //Input
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void MarioJump()
    {
 
        if (_isGrounded)
        {
            _marioRB.AddForce(_jumpHeight, ForceMode2D.Impulse);

        }
        

        
    }

    //Input
    private void MovementHandler()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MarioJump();
        }

        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _isRunning = true;
            Debug.Log("_isRunning is true");

        }
        else
        {
            _isRunning = false;
        }
        if (_isRunning)
        {
            _marioRB.MovePosition(_marioRB.position + (_moveInput * _runSpeed * Time.fixedDeltaTime));
        }
        else
        {
            _marioRB.MovePosition(_marioRB.position +  (_moveInput * _walkSpeed * Time.fixedDeltaTime));
        }
    }

    

    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(_groundCheck.position, Vector2.down, _rayLength, _groundLayer);
        _isGrounded = hit.collider != null;

        Debug.DrawRay(_groundCheck.position, Vector2.down * _rayLength, Color.red);
        MovementHandler();
    }
}
