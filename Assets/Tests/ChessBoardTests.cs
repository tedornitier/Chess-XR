using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class ChessBoardTests : MonoBehaviour
{
    [Test]
    public void IsWithinBoundsTest()
    {
        ChessBoard board = new ChessBoard();
        Assert.IsTrue(board.IsWithinBounds(0, 0));
        Assert.IsTrue(board.IsWithinBounds(7, 7));
        Assert.IsFalse(board.IsWithinBounds(-1, 0));
        Assert.IsFalse(board.IsWithinBounds(0, -1));
        Assert.IsFalse(board.IsWithinBounds(8, 0));
        Assert.IsFalse(board.IsWithinBounds(0, 8));
    }

    [Test]
    public void SetupBoardTest()
    {
        ChessBoard board = new ChessBoard();
        board.SetupBoard();
        board.PrintBoard();

        Assert.AreEqual(board.GetPieceAt(0, 0).type, ChessType.Rook);
        Assert.AreEqual(board.GetPieceAt(0, 0).color, ChessColor.Black);

        Assert.AreEqual(board.GetPieceAt(1, 0).type, ChessType.Knight);
        Assert.AreEqual(board.GetPieceAt(1, 0).color, ChessColor.Black);

        Assert.AreEqual(board.GetPieceAt(2, 0).type, ChessType.Bishop);
        Assert.AreEqual(board.GetPieceAt(2, 0).color, ChessColor.Black);

        Assert.AreEqual(board.GetPieceAt(3, 0).type, ChessType.Queen);
        Assert.AreEqual(board.GetPieceAt(3, 0).color, ChessColor.Black);

        Assert.AreEqual(board.GetPieceAt(4, 0).type, ChessType.King);
        Assert.AreEqual(board.GetPieceAt(4, 0).color, ChessColor.Black);

        Assert.AreEqual(board.GetPieceAt(5, 0).type, ChessType.Bishop);
        Assert.AreEqual(board.GetPieceAt(5, 0).color, ChessColor.Black);

        Assert.AreEqual(board.GetPieceAt(6, 0).type, ChessType.Knight);
        Assert.AreEqual(board.GetPieceAt(6, 0).color, ChessColor.Black);

        Assert.AreEqual(board.GetPieceAt(7, 0).type, ChessType.Rook);
        Assert.AreEqual(board.GetPieceAt(7, 0).color, ChessColor.Black);

        for (int i = 0; i < 8; i++)
        {
            Assert.AreEqual(board.GetPieceAt(i, 1).type, ChessType.Pawn);
            Assert.AreEqual(board.GetPieceAt(i, 1).color, ChessColor.Black);
        }

        Assert.AreEqual(board.GetPieceAt(0, 7).type, ChessType.Rook);
        Assert.AreEqual(board.GetPieceAt(0, 7).color, ChessColor.White);

        Assert.AreEqual(board.GetPieceAt(1, 7).type, ChessType.Knight);
        Assert.AreEqual(board.GetPieceAt(1, 7).color, ChessColor.White);

        Assert.AreEqual(board.GetPieceAt(2, 7).type, ChessType.Bishop);
        Assert.AreEqual(board.GetPieceAt(2, 7).color, ChessColor.White);

        Assert.AreEqual(board.GetPieceAt(3, 7).type, ChessType.Queen);
        Assert.AreEqual(board.GetPieceAt(3, 7).color, ChessColor.White);

        Assert.AreEqual(board.GetPieceAt(4, 7).type, ChessType.King);
        Assert.AreEqual(board.GetPieceAt(4, 7).color, ChessColor.White);

        Assert.AreEqual(board.GetPieceAt(5, 7).type, ChessType.Bishop);
        Assert.AreEqual(board.GetPieceAt(5, 7).color, ChessColor.White);

        Assert.AreEqual(board.GetPieceAt(6, 7).type, ChessType.Knight);
        Assert.AreEqual(board.GetPieceAt(6, 7).color, ChessColor.White);

        Assert.AreEqual(board.GetPieceAt(7, 7).type, ChessType.Rook);
        Assert.AreEqual(board.GetPieceAt(7, 7).color, ChessColor.White);

        for (int i = 0; i < 8; i++)
        {
            Assert.AreEqual(board.GetPieceAt(i, 6).type, ChessType.Pawn);
            Assert.AreEqual(board.GetPieceAt(i, 6).color, ChessColor.White);
        }
    }

    [Test]
    public void MovePieceTest()
    {
        ChessBoard board = new ChessBoard();
        board.SetupBoard();
        board.PrintBoard();

        ChessPiece blackKnight1 = board.GetPieceAt(1, 0);
        Assert.AreEqual(blackKnight1.type, ChessType.Knight);
        Assert.AreEqual(blackKnight1.color, ChessColor.Black);

        Debug.Log("Moving black knight from (1, 0) to (2, 2)");

        board.MovePiece((1, 0), (2, 2));
        board.PrintBoard();

        Assert.AreEqual(board.GetPieceAt(1, 0), null);
        Assert.AreEqual(board.GetPieceAt(2, 2).type, ChessType.Knight);
    }

    [Test]
    public void GetScoresTest()
    {
        ChessBoard board = new ChessBoard();
        Assert.AreEqual((0, 0), board.GetScores());
        board.SetupBoard();
        Assert.AreEqual((39, 39), board.GetScores());
        board.RemovePiece((0, 0)); // black rook, 5 points
        Assert.AreEqual((39, 34), board.GetScores());
        board.RemovePiece((0, 1)); // black pawn, 1 point
        Assert.AreEqual((39, 33), board.GetScores());
        board.RemovePiece((3, 7)); // white queen, 9 points
        Assert.AreEqual((30, 33), board.GetScores());
        board.RemovePiece((1, 7)); // white knight, 3 points
        Assert.AreEqual((27, 33), board.GetScores());
        board.RemovePiece((2, 0)); // black bishop, 3 points
        Assert.AreEqual((27, 30), board.GetScores());
    }
}
