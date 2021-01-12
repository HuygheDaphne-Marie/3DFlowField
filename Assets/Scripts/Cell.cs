using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector3 worldPos;
    public Vector3Int gridCoordinates;
    public byte cost;

    public Cell(Vector3 _worldpos, Vector3Int _gridCoords, byte _cost)
    {
        worldPos = _worldpos;
        gridCoordinates = _gridCoords;
        cost = _cost;
    }
}
