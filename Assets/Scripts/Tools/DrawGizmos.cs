using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    // TODO Add posibility to select shape or link icon
    [SerializeField] private Color _color;
    [SerializeField] private float _size;
    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawWireSphere(transform.position, _size);
    }
}
