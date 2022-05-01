using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _jumpForce;

    private int _state = 0;
    //0 - down
    //1 - up
    //2 - left
    //3 - right

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Animator _anim;

    private Vector3 _rotationVector;

    private float _horizontalInput = 0f;

    public bool ist;

    #region InputCollbacks

    public void INPMoving(InputAction.CallbackContext context)
    {
        _horizontalInput = context.ReadValue<Vector2>().x;
    }

    public void INPJump(InputAction.CallbackContext context)
    {
        if (PlayerGroundChecker.isGrounded)
        {
            switch (_state)
            {
                case 0:
                    {
                        _rb.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Force);
                        break;
                    }
                case 1:
                    {
                        _rb.AddForce(new Vector2(0f, -_jumpForce), ForceMode2D.Force);
                        break;
                    }
                case 2:
                    {
                        _rb.AddForce(new Vector2(_jumpForce, 0f), ForceMode2D.Force);
                        break;
                    }
                case 3:
                    {
                        _rb.AddForce(new Vector2(-_jumpForce, 0f), ForceMode2D.Force);
                        break;
                    }
            }
        }
    }

    public void INPUpGChange(InputAction.CallbackContext context)
    {
        if ((_state == 0)||(!PlayerGroundChecker.isGrounded)) return;

        Physics2D.gravity = new Vector2(0f, _gravityScale);

        _state = 0;
        _rotationVector.z = 0f;
    }

    public void INPDownGChange(InputAction.CallbackContext context)
    {
        if ((_state == 1) || (!PlayerGroundChecker.isGrounded)) return;

        Physics2D.gravity = new Vector2(0f, -_gravityScale);

        _state = 1;
        _rotationVector.z = 180f;
    }

    public void INPLeftGChange(InputAction.CallbackContext context)
    {
        if ((_state == 2) || (!PlayerGroundChecker.isGrounded)) return;

        Physics2D.gravity = new Vector2(-_gravityScale, 0f);

        _state = 2;
        _rotationVector.z = -90f;
    }

    public void INPRightGChange(InputAction.CallbackContext context)
    {
        if ((_state == 3) || (!PlayerGroundChecker.isGrounded)) return;

        Physics2D.gravity = new Vector2(_gravityScale, 0f);

        _state = 3;
        _rotationVector.z = 90f;
    }

    #endregion

    private void Awake()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _sr = this.GetComponent<SpriteRenderer>();
        _anim = this.GetComponent<Animator>();
    }

    private void Start()
    {
        Physics2D.gravity = new Vector2(0f, -_gravityScale);


    }

    private void Update()
    {
        ist = PlayerGroundChecker.isGrounded;
        if (_horizontalInput != 0)
        {
            
        }

        if (_horizontalInput > 0)
        {
            _sr.flipX = false;
        }
        else if (_horizontalInput < 0)
        {
            _sr.flipX = true;
        }


    }

    private void FixedUpdate()
    {
        switch (_state)
        {
            case 0:
                {
                    _rb.velocity = new Vector2(_speed * _horizontalInput, _rb.velocity.y);
                    break;
                }
            case 1:
                {
                    _rb.velocity = new Vector2(-_speed * _horizontalInput, _rb.velocity.y);
                    break;
                }
            case 2:
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, -_speed * _horizontalInput);
                    break;
                }
            case 3:
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, _speed * _horizontalInput);
                    break;
                }
        }
    }

}
