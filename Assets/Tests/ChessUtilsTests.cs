using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChessUtilsTests
{
    [Test]
    public void CalculateCellPositionTest()
    {
        Vector3 position = new Vector3(-3.5f, 0, -3.5f);
        float playAreaLength = 8;
        (int, int) result = ChessUtils.CalculateCellPosition(position, playAreaLength);
        Assert.AreEqual((0, 7), result);

        position = new Vector3(-4, 0, 4);
        result = ChessUtils.CalculateCellPosition(position, playAreaLength);
        Assert.AreEqual((0, 0), result);

        position = new Vector3(3.9f, 0, -3.9f);
        result = ChessUtils.CalculateCellPosition(position, playAreaLength);
        Assert.AreEqual((7, 7), result);

        position = new Vector3(3.9f, 0, 4f);
        result = ChessUtils.CalculateCellPosition(position, playAreaLength);
        Assert.AreEqual((7, 0), result);

        playAreaLength = 16;
        position = new Vector3(-8, 0, 8);
        result = ChessUtils.CalculateCellPosition(position, playAreaLength);
        Assert.AreEqual((0, 0), result);
    }

    [Test]
    public void GetPieceCoordinateFromCellTest()
    {
        float playAreaLength = 8;

        (int, int) cell = (0, 0);
        (double, double) result = ChessUtils.GetPieceCoordinateFromCell(cell, playAreaLength);
        Assert.AreEqual((-3.5, 3.5), result);

        cell = (7, 7);
        result = ChessUtils.GetPieceCoordinateFromCell(cell, playAreaLength);
        Assert.AreEqual((3.5, -3.5), result);

        cell = (7, 0);
        result = ChessUtils.GetPieceCoordinateFromCell(cell, playAreaLength);
        Assert.AreEqual((3.5, 3.5), result);

        cell = (0, 7);
        result = ChessUtils.GetPieceCoordinateFromCell(cell, playAreaLength);
        Assert.AreEqual((-3.5, -3.5), result);

        cell = (2, 5);
        result = ChessUtils.GetPieceCoordinateFromCell(cell, playAreaLength);
        Assert.AreEqual((-1.5, -1.5), result);
    }
}