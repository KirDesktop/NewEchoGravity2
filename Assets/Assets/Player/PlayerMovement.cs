using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement ton;

    [Header("Player Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _jumpForce;

    [SerializeField] private float _jumpWaitTime;

    [SerializeField] private GameObject _jumpEffect;
    [SerializeField] private GameObject _changeGravityEffect;

    private int _state = 1;
    //0 - up
    //1 - down
    //2 - left
    //3 - right

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Animator _anim;

    private Vector3 _rotationVector;

    private bool _animationCheck = false;

    [HideInInspector] public float _horizontalInput = 0f;

    #region InputCollbacks

    public void INPMoving(InputAction.CallbackContext context)
    {
        if (this.GetComponent<PlayerLife>().isDeath) return;
        _horizontalInput = context.ReadValue<Vector2>().x;
    }

    public void INPJump(InputAction.CallbackContext context)
    {
        if (this.GetComponent<PlayerLife>().isDeath) return;
        if (!PlayerGroundChecker.isGrounded) return;
        if (!context.performed) return;

        Instantiate(_jumpEffect, this.transform);

        _animationCheck = false;
        _anim.SetTrigger("Jump");
        StartCoroutine(_setUpdateJumpAnimations(true));

        switch (_state)
        {
            case 0:
                {
                    _rb.AddForce(new Vector2(0f, -_jumpForce), ForceMode2D.Force);
                    break;
                }
            case 1:
                {
                    _rb.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Force);
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

    public void INPUpGChange(InputAction.CallbackContext context)
    {
        if (this.GetComponent<PlayerLife>().isDeath) return;
        if ((_state == 0)||(!PlayerGroundChecker.isGrounded)) return;

        Instantiate(_changeGravityEffect, this.transform);

        Physics2D.gravity = new Vector2(0f, _gravityScale);

        _state = 0;
        _rotationVector.z = 180f;
    }

    public void INPDownGChange(InputAction.CallbackContext context)
    {
        if (this.GetComponent<PlayerLife>().isDeath) return;
        if ((_state == 1) || (!PlayerGroundChecker.isGrounded)) return;

        Instantiate(_changeGravityEffect, this.transform);

        Physics2D.gravity = new Vector2(0f, -_gravityScale);

        _state = 1;
        _rotationVector.z = 0f;
    }

    public void INPLeftGChange(InputAction.CallbackContext context)
    {
        if (this.GetComponent<PlayerLife>().isDeath) return;
        if ((_state == 2) || (!PlayerGroundChecker.isGrounded)) return;

        Instantiate(_changeGravityEffect, this.transform);

        Physics2D.gravity = new Vector2(-_gravityScale, 0f);

        _state = 2;
        _rotationVector.z = -90f;
    }

    public void INPRightGChange(InputAction.CallbackContext context)
    {
        if (this.GetComponent<PlayerLife>().isDeath) return;
        if ((_state == 3) || (!PlayerGroundChecker.isGrounded)) return;

        Instantiate(_changeGravityEffect, this.transform);

        Physics2D.gravity = new Vector2(_gravityScale, 0f);

        _state = 3;
        _rotationVector.z = 90f;
    }

    private IEnumerator _setUpdateJumpAnimations(bool ist)
    {
        yield return new WaitForSeconds(_jumpWaitTime);

        _animationCheck = ist;
    }

    #endregion

    private void Awake()
    {
        if (ton == null)
        {
            ton = this;
        }

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
        if (this.GetComponent<PlayerLife>().isDeath) return;

        if (_animationCheck) _anim.SetBool("isGrounded", PlayerGroundChecker.isGrounded ? true : false);
        else _anim.SetBool("isGrounded", false);

        if (_horizontalInput != 0)
        {
            _anim.SetBool("isIdle",false);
        }
        else
        {
            _anim.SetBool("isIdle", true);
        }

        if (_horizontalInput > 0)
        {
            _sr.flipX = _state == 0 ? true : false;
        }
        else if (_horizontalInput < 0)
        {
            _sr.flipX = _state == 0 ? false : true;
        }

        this.transform.rotation = Quaternion.Euler(_rotationVector);
    }

    private void FixedUpdate()
    {
        if (this.GetComponent<PlayerLife>().isDeath) return;
        switch (_state)
        {
            case 0:
                {
                    _rb.velocity = new Vector2(_speed * _horizontalInput, _rb.velocity.y);
                    break;
                }
            case 1:
                {
                    _rb.velocity = new Vector2(_speed * _horizontalInput, _rb.velocity.y);
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
