using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector3 worldPos;
    public Vector3Int coordinates;

    public byte cost;
    public uint bestCost;
    public Vector3 bestDirection;

    public Cell(Vector3 _worldpos, Vector3Int _gridCoords)
    {
        worldPos = _worldpos;
        coordinates = _gridCoords;
        cost = 1;
        bestCost = uint.MaxValue;
        bestDirection = Vector3.zero;
    }

    public void IncreaseCost(byte _amount)
    {
        if (cost == byte.MaxValue)
        {
            return;
        }

        if (cost + _amount > byte.MaxValue)
        {
            cost = byte.MaxValue;
        }
        else
        {
            cost += _amount;
        }
    }
}
