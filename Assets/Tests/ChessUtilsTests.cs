using NUnit.Framework;
using System;
using System.Collections.Generic;

public class ChessUtilsTests
{
    [Test]
    public void TestAdd()
    {
        Assert.AreEqual(3, ChessUtils.Add(1, 2));
    }
}