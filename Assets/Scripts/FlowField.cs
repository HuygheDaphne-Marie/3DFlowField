using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField : MonoBehaviour
{
    // TEMP
    public GameObject player;

    public LayerMask NonTraverseableMask;
    public Vector3 flowFieldWorldSize;
    public float cellRadius;
    Cell[,,] cells;

    Vector3Int flowFieldSize;
    Vector3 flowFieldBottomLeft;

    float cellDiameter;

    void Start()
    {
        cellDiameter = cellRadius * 2;
        flowFieldSize.x = Mathf.CeilToInt(flowFieldWorldSize.x / cellDiameter);
        flowFieldSize.y = Mathf.CeilToInt(flowFieldWorldSize.y / cellDiameter);
        flowFieldSize.z = Mathf.CeilToInt(flowFieldWorldSize.z / cellDiameter);

        CreateFlowField();
    }

    void CreateFlowField()
    {
        cells = new Cell[flowFieldSize.x, flowFieldSize.y, flowFieldSize.z];
        flowFieldBottomLeft = transform.position;
        flowFieldBottomLeft -= Vector3.right * flowFieldWorldSize.x / 2;
        flowFieldBottomLeft -= Vector3.up * flowFieldWorldSize.y / 2;
        flowFieldBottomLeft -= Vector3.forward * flowFieldWorldSize.z / 2;

        for (int x = 0; x < flowFieldSize.x; x++)
        {
            for (int y = 0; y < flowFieldSize.y; y++)
            {
                for (int z = 0; z < flowFieldSize.z; z++)
                {
                    Vector3 cellWorldPos = flowFieldBottomLeft;
                    cellWorldPos += Vector3.right * (x * cellDiameter + cellRadius);
                    cellWorldPos += Vector3.up * (y * cellDiameter + cellRadius);
                    cellWorldPos += Vector3.forward * (z * cellDiameter + cellRadius);

                    byte cost = 0; // TODO: move to cell, so it can handle cost calculation
                    bool traverseable = !(Physics.CheckSphere(cellWorldPos, cellRadius, NonTraverseableMask));
                    if(!traverseable)
                    {
                        cost = 255;
                    }

                    Vector3Int cellsPos = new Vector3Int(x, y, z);
                    cells[x, y, z] = new Cell(cellWorldPos, cellsPos, cost);
                }
            }
        }

    }

    public Cell CellFromWorldPos(Vector3 worldPos)
    {
        Vector3 bottomLeftCellPos = flowFieldBottomLeft + Vector3.one * cellRadius;
        Vector3 bottomLeftToWorldPos = worldPos - bottomLeftCellPos;

        int x, y, z;
        x = Mathf.Clamp(Mathf.RoundToInt(bottomLeftToWorldPos.x / cellDiameter), 0, flowFieldSize.x - 1);
        y = Mathf.Clamp(Mathf.RoundToInt(bottomLeftToWorldPos.y / cellDiameter), 0, flowFieldSize.y - 1);
        z = Mathf.Clamp(Mathf.RoundToInt(bottomLeftToWorldPos.z / cellDiameter), 0, flowFieldSize.z - 1);
        return cells[x, y, z];
    }

    bool IsCellPosWithinBounds(Vector3Int cellPos)
    {
        if (cellPos.x < 0 || cellPos.x >= flowFieldSize.x)
        {
            return false;
        }
        if (cellPos.y < 0 || cellPos.y >= flowFieldSize.y)
        {
            return false;
        }
        if (cellPos.z < 0 || cellPos.z >= flowFieldSize.z)
        {
            return false;
        }
        return true;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, flowFieldWorldSize);

        if(cells != null)
        {
            Cell playerCell = CellFromWorldPos(player.transform.position);
            bool firstCell = true;
            foreach (Cell cell in cells)
            {
                Color cellColor = Color.white;
                cellColor.a = 0.001f;

                if (cell.cost == 255)
                {
                    cellColor = Color.red;
                }

                // TEMP
                if (cell == playerCell)
                {
                    cellColor = Color.cyan;
                }
                if (firstCell)
                {
                    cellColor = Color.yellow;
                    firstCell = false;
                }

                Gizmos.color = cellColor;

                Gizmos.DrawCube(cell.worldPos, Vector3.one * (cellDiameter - 0.1f));
            }
        }
    }
}
