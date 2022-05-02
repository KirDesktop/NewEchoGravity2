using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private Transform _playerTop;
    [SerializeField] private GameObject _gun;
    [SerializeField] private GameObject _deathEffect;

    [Header("Health Settings")]
    [SerializeField] private int _health = 1;
    [SerializeField] private float _invulnerabilityTime;
    [SerializeField] private float _deathTime;

    [Header("Damage Settings")]
    [SerializeField] private int _spikeDamage;

    public bool isDeath = false;

    private float _timeToDamage;

    private void _takeDamage(int damage)
    {
        if (Time.time >= _timeToDamage)
        {
            _timeToDamage = Time.time + _invulnerabilityTime;

            _health -= damage;

            if (_health <= 0)
            {
                _death();
            }
        }
    }

    public void INPDeath(InputAction.CallbackContext context)
    {
        _death();
    }

    private void _death()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<PlayerMovement>().enabled = false;
        this.GetComponent<Rigidbody2D>().simulated = false;
        _gun.SetActive(false);
        isDeath = true;

        Instantiate(_deathEffect, this.transform.position,Quaternion.identity);

        Invoke("_restartLevel", _deathTime);
    }


    private void _restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            _takeDamage(_spikeDamage);
        }

        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            this.transform.parent = collision.transform.parent.transform.Find("Platform");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            this.transform.parent = _playerTop.transform.parent;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rune"))
        {
            RuneController.ton.activateRune();
        }

        if (collision.CompareTag("Portal"))
        {
            PortalController.ton.activatePortal();
        }
    }

    
}
