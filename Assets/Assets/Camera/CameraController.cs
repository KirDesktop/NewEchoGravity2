using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Update()
    {
        this.transform.position = new Vector3(PlayerMovement.ton.transform.position.x, PlayerMovement.ton.transform.position.y, this.transform.position.z); ;
    }
}
