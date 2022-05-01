using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneController : MonoBehaviour
{
    public static RuneController ton;

    private Animator _anim;

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
        PortalController.ton.openPortal();

    }

    public void destroy()
    {
        Destroy(this.gameObject);
    }
}
