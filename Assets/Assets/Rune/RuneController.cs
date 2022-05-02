using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneController : MonoBehaviour
{
    [SerializeField] private GameObject _openPortalEffect;
    public static RuneController ton;

    private Animator _anim;

    private bool _isActivate = false;

    private void Awake()
    {
        if (ton == null)
        {
            ton = this;
        }

        _anim = this.GetComponent<Animator>();
    }

    public void activateRune()
    {
        if (_isActivate) return;

        PortalController.ton.openPortal();
        Instantiate(_openPortalEffect, this.transform);

        _anim.SetTrigger("destroy");

        _isActivate = true;
    }

    public void destroy()
    {
        Destroy(this.gameObject);
    }
}
