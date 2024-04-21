using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class ChessAITests
{
    [Test]
    public void QueenMoves_CapturesPawn_DoesntGetCaptured()
    {
        ChessAI ai = new ChessAI(ChessColor.White, 2);

        ChessBoard board = new ChessBoard();
        foreach (var position in new List<(int, int)> {
            (1, 1), (2, 1), (0, 2), (1, 3), (1, 4), (0, 5), (2, 3)
        }) {
            board.SetPieceAt(position, new ChessPiece(ChessType.Pawn, ChessColor.Black));
        }
        board.SetPieceAt((0, 3), new ChessPiece(ChessType.Queen, ChessColor.White));
        board.PrintBoard();

        List<(int, int, int, int)> legalMoves = ai.GenerateLegalMoves(board);
        List<(int, int, int, int)> expectedLegalMoves = new List<(int, int, int, int)> {
            (0, 3, 0, 2), (0, 3, 1, 2), (0, 3, 2, 1), (0, 3, 1, 3),
            (0, 3, 1, 4), (0, 3, 0, 4), (0, 3, 0, 5)
        };
        Assert.AreEqual(expectedLegalMoves.Count, legalMoves.Count);
        foreach (var move in expectedLegalMoves)
        {
            if (!legalMoves.Contains(move))
            {
                Debug.Log("Expected move: " + move + " not found");
            }
            Assert.IsTrue(legalMoves.Contains(move));
        }

        (int, int, int, int) bestMove = ai.GetBestMove(board);
        (int, int, int, int) expectedBestMove = (0, 3, 2, 1);
        Assert.AreEqual(expectedBestMove, bestMove);
    }

    [Test]
    public void WhitePawnMoves_CapturesBlackPawn_DoesntGetCaptured()
    {
        ChessAI ai = new ChessAI(ChessColor.White, 2);

        ChessBoard board = new ChessBoard();
        board.SetPieceAt((0, 2), new ChessPiece(ChessType.Pawn, ChessColor.Black));
        board.SetPieceAt((1, 3), new ChessPiece(ChessType.Pawn, ChessColor.Black));
        board.SetPieceAt((3, 3), new ChessPiece(ChessType.Pawn, ChessColor.Black));
        board.SetPieceAt((2, 4), new ChessPiece(ChessType.Pawn, ChessColor.White));
        board.PrintBoard();

        (int, int, int, int) bestMove = ai.GetBestMove(board);
        (int, int, int, int) expectedBestMove = (2, 4, 3, 3);
        Assert.AreEqual(expectedBestMove, bestMove);
    }

    [Test]
    public void WhiteKnightMoves_DoesntCapture_DoesntGetCaptured()
    {
        ChessAI ai = new ChessAI(ChessColor.White, 2);

        ChessBoard board = new ChessBoard();
        board.SetPieceAt((7, 7), new ChessPiece(ChessType.Knight, ChessColor.White));
        board.SetPieceAt((4, 5), new ChessPiece(ChessType.Pawn, ChessColor.Black));
        board.SetPieceAt((5, 6), new ChessPiece(ChessType.Pawn, ChessColor.Black));
        board.PrintBoard();

        (int, int, int, int) bestMove = ai.GetBestMove(board);
        (int, int, int, int) expectedBestMove = (7, 7, 6, 5);
        Assert.AreEqual(expectedBestMove, bestMove);
    }

    [Test]
    public void GetPawnChainLengthTest()
    {
        ChessAI ai = new ChessAI(ChessColor.White, 2);
        ChessBoard board = new ChessBoard();
        ChessPiece pawn = new ChessPiece(ChessType.Pawn, ChessColor.White);
        board.SetPieceAt((0, 0), pawn);
        board.SetPieceAt((1, 1), new ChessPiece(ChessType.Pawn, ChessColor.White));
        board.SetPieceAt((2, 2), new ChessPiece(ChessType.Pawn, ChessColor.White));

        ChessPiece otherPawn = new ChessPiece(ChessType.Pawn, ChessColor.White);
        board.SetPieceAt((4, 4), otherPawn);
        board.SetPieceAt((5, 3), new ChessPiece(ChessType.Pawn, ChessColor.White));

        board.PrintBoard();

        Assert.AreEqual(3, ai.GetPawnChainLength(board, pawn));
        Assert.AreEqual(2, ai.GetPawnChainLength(board, otherPawn));
    }

    [Test]
    public void IsCheckTest()
    {
        ChessAI ai = new ChessAI(ChessColor.White, 2);
        ChessBoard board = new ChessBoard();
        board.SetPieceAt((0, 0), new ChessPiece(ChessType.King, ChessColor.White));
        board.SetPieceAt((1, 1), new ChessPiece(ChessType.Rook, ChessColor.Black));
        board.SetPieceAt((1, 2), new ChessPiece(ChessType.King, ChessColor.Black));
        board.PrintBoard();
        Assert.IsFalse(ai.IsCheck(board));

        board.MovePiece((1, 1), (1, 0));
        board.PrintBoard();
        Assert.IsTrue(ai.IsCheck(board));
    }

    [Test]
    public void EvaluateControlOfCenterTest()
    {
        // center squares are (3, 3), (3, 4), (4, 3), (4, 4)
        // the values for each piece in the center are:
        // pawn: 1, knight: 3, bishop: 3, rook: 2, queen: 4, king: 5
        ChessAI ai = new ChessAI(ChessColor.White, 2);
        ChessBoard board = new ChessBoard();
        Assert.AreEqual(0, ai.EvaluateControlOfCenter(board, ChessColor.White));
        board.SetPieceAt((3, 3), new ChessPiece(ChessType.Pawn, ChessColor.White)); // +1
        board.PrintBoard();
        Assert.AreEqual(1, ai.EvaluateControlOfCenter(board, ChessColor.White));
        board.SetPieceAt((3, 4), new ChessPiece(ChessType.Knight, ChessColor.White)); // +3
        board.PrintBoard();
        Assert.AreEqual(4, ai.EvaluateControlOfCenter(board, ChessColor.White));
        board.SetPieceAt((4, 3), new ChessPiece(ChessType.Bishop, ChessColor.White)); // +3
        board.PrintBoard();
        Assert.AreEqual(7, ai.EvaluateControlOfCenter(board, ChessColor.White));
        board.SetPieceAt((4, 4), new ChessPiece(ChessType.Rook, ChessColor.White)); // +2
        board.PrintBoard();
        Assert.AreEqual(9, ai.EvaluateControlOfCenter(board, ChessColor.White));
        board.SetPieceAt((3, 3), new ChessPiece(ChessType.Queen, ChessColor.White)); // replacing pawn (1) with queen (4), +3
        board.PrintBoard();
        Assert.AreEqual(12, ai.EvaluateControlOfCenter(board, ChessColor.White));
        board.SetPieceAt((3, 4), new ChessPiece(ChessType.Pawn, ChessColor.Black)); // removing knight (3), -3
        board.PrintBoard();
        Assert.AreEqual(9, ai.EvaluateControlOfCenter(board, ChessColor.White));
    }
}
