using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChessUtilsTests
{
    [Test]
    public void CalculateCellPositionTest()
    {
        Vector3 position = new Vector3(0, 0, 0);
        float playAreaLength = 8;
        (int, int) result = ChessUtils.CalculateCellPosition(position, playAreaLength);
        Assert.AreEqual((4, 4), result);

        position = new Vector3(-4, 0, -4);
        result = ChessUtils.CalculateCellPosition(position, playAreaLength);
        Assert.AreEqual((0, 0), result);

        position = new Vector3(3.9f, 0, 3.9f);
        result = ChessUtils.CalculateCellPosition(position, playAreaLength);
        Assert.AreEqual((7, 7), result);

        position = new Vector3(3.9f, 0, -4f);
        result = ChessUtils.CalculateCellPosition(position, playAreaLength);
        Assert.AreEqual((7, 0), result);
    }
}