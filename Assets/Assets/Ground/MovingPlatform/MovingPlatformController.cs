using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;

    [SerializeField] private float _minDistance;

    [SerializeField] private float _speed;
    private bool _is1 = false;

    private void FixedUpdate()
    {
        if (_is1)
        {
            if(Vector2.Distance(this.transform.position, _point1.position) <= _minDistance)
            {
                _is1 = false;
            }
            else
            {
                this.transform.Translate((_point1.position - this.transform.position).normalized * _speed);
            }
        }
        else
        {
            if (Vector2.Distance(this.transform.position, _point2.position) <= _minDistance)
            {
                _is1 = true;
            }
            else
            {
                this.transform.Translate((_point2.position - this.transform.position).normalized * _speed);
            }
        }
    }
}
