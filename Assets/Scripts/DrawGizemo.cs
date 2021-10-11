using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizemo : MonoBehaviour
{
    public float radius = 2;
    public Color color=Color.green;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
