using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    public static PortalController ton;

    [SerializeField] private GameObject _portalEffect;

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

        _loadNewLevel();
    }

    private void _loadNewLevel()
    {
        Debug.Log("load");
    }

    public void openPortal()
    {
        isOpen = true;

        _portalEffect.SetActive(true);
    }
}
