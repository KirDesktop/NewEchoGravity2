using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunController : MonoBehaviour
{
    [Header("Gun Settings")]
    [SerializeField] private float _reloadTime;

    [Header("Gun References")]
    [SerializeField] private GameObject _echoBullet;
    [SerializeField] private Transform _gunPivotRight;
    [SerializeField] private Transform _gunPivotLeft;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _shootEffect;

    private float _timeToShoot;

    private SpriteRenderer _sr;

    private float _angle;
    public float a;

    private bool _isChanged = false;

    private bool _isRight = true;

    private void Awake()
    {
        _sr = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!_isChanged)
        {
            this.transform.position = _gunPivotRight.transform.position;
            //this.transform.parent = _gunPivotRight.transform;
        }

        if (PlayerMovement.ton._horizontalInput == 0)
        {
            if (_isRight)
            {
                this.transform.position = _gunPivotRight.transform.position;
            }
            else
            {
                this.transform.position = _gunPivotLeft.transform.position;
            }
        }

        if (PlayerMovement.ton._horizontalInput > 0)
        {
            this.transform.position = _gunPivotRight.transform.position;
            _isRight = true;
            //this.transform.parent = _gunPivotRight.transform;
        }
        else if (PlayerMovement.ton._horizontalInput < 0)
        {
            this.transform.position = _gunPivotLeft.transform.position;
            _isRight = false;
            //this.transform.parent = _gunPivotLeft.transform;
            _isChanged = true;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 dir = mousePos - new Vector2(_firePoint.transform.position.x, _firePoint.transform.position.y);
        _angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _firePoint.transform.rotation = Quaternion.Euler(0f, 0f, _angle - 90f);
        this.transform.rotation = Quaternion.Euler(0f, 0f, _angle);

         a = this.transform.eulerAngles.z;


        if (((a >= 0) && (a <= 90)) || ((a >= 270) && (a <= 360)))
        {
            _sr.flipY = false;
        }
        else
        {
            _sr.flipY = true;
        }
    }

    public void INPShoot(InputAction.CallbackContext context)
    {
        if(Time.time >= _timeToShoot)
        {
            _timeToShoot = Time.time + _reloadTime;

            GameObject e = Instantiate(_shootEffect, this.transform.position, Quaternion.Euler(0f, 0f, _angle));
            e.transform.parent = this.transform;
            GameObject echo = Instantiate(_echoBullet, _firePoint.position, _firePoint.rotation);
            echo.transform.parent = _firePoint.transform;
        }
    }
}
