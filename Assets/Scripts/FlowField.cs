using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField : MonoBehaviour
{
    public Vector3 flowFieldWorldSize;
    public float cellRadius;
    
    Cell[,,] cells;

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, flowFieldWorldSize);
    }
}
