using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessUtils
{
    public static (int, int) CalculateCellPosition(Vector3 position, float playAreaLength) {
        int x = (int) Math.Floor(8 * position.x / playAreaLength) + 4;
        int z = (int) Math.Floor(8 * position.z / playAreaLength) + 4;
        return (x, z);
    }

    public static (double, double) GetPieceCoordinateFromCell((int, int) cell, float playAreaLength)
    {
        double cellLength = playAreaLength / 8;
        (int x, int z) = cell;
        double positionX = (x - 4 + 0.5) * cellLength;
        double positionZ = (z - 4 + 0.5) * cellLength;
        return (positionX, positionZ);
    }
}