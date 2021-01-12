using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector3 worldPos;
    public Vector3Int gridCoordinates;

    public Cell(Vector3 _worldpos, Vector3Int _gridCoords)
    {
        worldPos = _worldpos;
        gridCoordinates = _gridCoords;
    }
}
