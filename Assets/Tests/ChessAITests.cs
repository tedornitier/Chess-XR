using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class ChessAITests
{
    [Test]
    public void Test()
    {
        ChessAI ai = new ChessAI(ChessColor.Black, 3);
        ChessBoard board = new ChessBoard();
        board.SetupBoard();
        board.PrintBoard();

        List<(int, int, int, int)> legalMoves = ai.GenerateLegalMoves(board);
        Assert.IsFalse(legalMoves.Contains((7, 1, 5, 7)));
        Assert.IsFalse(legalMoves.Contains((7, 1, 7, 7)));
        Assert.IsTrue(legalMoves.Contains((7, 1, 7, 2)));
        Assert.IsTrue(legalMoves.Contains((7, 1, 7, 3)));

        board.MovePiece((3, 6), (3, 4));
        board.PrintBoard();

        (int, int, int, int) move = ai.GetBestMove(board);
        board.MovePiece((move.Item1, move.Item2), (move.Item3, move.Item4));
        board.PrintBoard();

        board.MovePiece((4, 6), (4, 4));
        board.PrintBoard();

        move = ai.GetBestMove(board);
        board.MovePiece((move.Item1, move.Item2), (move.Item3, move.Item4));
        board.PrintBoard();

        board.MovePiece((5, 6), (5, 5));
        board.PrintBoard();

        legalMoves = ai.GenerateLegalMoves(board);
        Assert.IsFalse(legalMoves.Contains((7, 1, 5, 7)));
        Assert.IsFalse(legalMoves.Contains((7, 1, 7, 7)));
        Assert.IsTrue(legalMoves.Contains((7, 1, 7, 2)));
        Assert.IsTrue(legalMoves.Contains((7, 1, 7, 3)));
        // foreach (var item in legalMoves) { Debug.Log(item.Item1 + "," + item.Item2 + " -> " + item.Item3 + "," + item.Item4); }

        move = ai.GetBestMove(board);
        board.MovePiece((move.Item1, move.Item2), (move.Item3, move.Item4));
        board.PrintBoard();
    }
}
