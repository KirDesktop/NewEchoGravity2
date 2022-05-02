using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundChecker : MonoBehaviour
{
    public static bool isGrounded = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Rune")) return;
        if (collision.CompareTag("Portal")) return;

        if (LayerMask.LayerToName(collision.gameObject.layer) == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Rune")) return;
        if (collision.CompareTag("Portal")) return;

        if (LayerMask.LayerToName(collision.gameObject.layer) == "Ground")
        {
            isGrounded = false;
        }
    }
}
