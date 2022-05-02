using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    public static PortalController ton;

    [SerializeField] private GameObject _portalEffect;
    [SerializeField] private GameObject _openStateEffect;

    [SerializeField] private float _transferTime;
    [SerializeField] private GameObject _transferEffect;

    public bool isOpen = false;

    private void Awake()
    {
        if (ton == null)
        {
            ton = this;
        }
    }

    public void activatePortal()
    {
        if (!isOpen) return;

        Instantiate(_transferEffect);
        Invoke("_loadNewLevel", _transferTime);
    }

    private void _loadNewLevel()
    {
        // Debug.Log("load");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void openPortal()
    {
        isOpen = true;

        _portalEffect.SetActive(true);
        _openStateEffect.SetActive(true);
    }
}
