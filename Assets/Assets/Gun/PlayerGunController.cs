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
    [SerializeField] private Transform _gunPivot;
    [SerializeField] private Transform _firePoint;

    private float _timeToShoot;

    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        this.transform.position = _gunPivot.transform.position;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 dir = mousePos - new Vector2(_firePoint.transform.position.x, _firePoint.transform.position.y);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _firePoint.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    public void INPShoot(InputAction.CallbackContext context)
    {
        if(Time.time >= _timeToShoot)
        {
            _timeToShoot = Time.time + _reloadTime;

            GameObject echo = Instantiate(_echoBullet, _firePoint.position, _firePoint.rotation);
            echo.transform.parent = _firePoint.transform;
        }
    }
}
