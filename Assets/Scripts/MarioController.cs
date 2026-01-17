using UnityEngine;
using UnityEngine.InputSystem;

public class MarioController : MonoBehaviour
{
    public Rigidbody2D _marioRB;
    private Vector2 _moveInput;

    private bool _isRunning;
    private bool _isJumping;
    

    [Header("Walk/Run Values")]
    public float _walkSpeed;
    public float _runSpeed;

    [Header("Jumping Values")]
    public float _jumpHeight;

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    

    private void MovementHandler()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJumping = true;
            Debug.Log("_isJumping is true");
        }
        else
        {
            _isJumping = false;
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

        if (_isJumping)
        {
            _marioRB.MovePosition(_marioRB.position + (_moveInput * _walkSpeed * _jumpHeight * Time.fixedDeltaTime));
            _marioRB.AddForce(_moveInput * _walkSpeed * _jumpHeight * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        else if (_isJumping && _isRunning)
        {
            _marioRB.MovePosition(_marioRB.position + (_moveInput * _runSpeed * _jumpHeight * Time.fixedDeltaTime));
            _marioRB.AddForce(_moveInput * _jumpHeight * Time.fixedDeltaTime, ForceMode2D.Impulse);

        }
        else if (_isRunning)
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
        MovementHandler();
    }
}
