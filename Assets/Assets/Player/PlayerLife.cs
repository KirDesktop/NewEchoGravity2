using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int _health = 3;
    [SerializeField] private float _invulnerabilityTime;
    [SerializeField] private float _deathTime;

    [Header("Damage Settings")]
    [SerializeField] private int _spikeDamage;

    private float _timeToDamage;

    private void _takeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            _death();
        }
    }

    public void INPDeath(InputAction.CallbackContext context)
    {
        _death();
    }

    private void _death()
    {


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
    }
}
