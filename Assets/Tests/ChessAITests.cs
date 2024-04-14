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

        List<(int, int, int, int)> legalMoves = ai.GenerateLegalMoves(board, ChessColor.White);
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
    public void EvaluateTest()
    {
        ChessAI chessAI = new ChessAI(ChessColor.White, 2);
        ChessBoard board = new ChessBoard();
        foreach (var position in new List<(int, int)> {
            (1, 1), (2, 1), (0, 2), (1, 3), (1, 4), (0, 5), (2, 3)
        }) {
            board.SetPieceAt(position, new ChessPiece(ChessType.Pawn, ChessColor.Black));
        }
        board.SetPieceAt((0, 3), new ChessPiece(ChessType.Queen, ChessColor.White));
        board.PrintBoard();

        int expectedScore = 2;
        Assert.AreEqual(expectedScore, chessAI.Evaluate(board));

        board.MovePiece((0, 3), (0, 2));
        board.PrintBoard();
        expectedScore = 3;
        Assert.AreEqual(expectedScore, chessAI.Evaluate(board));

        board.MovePiece((2, 1), (0, 2));
        board.PrintBoard();
        expectedScore = -6;
        Assert.AreEqual(expectedScore, chessAI.Evaluate(board));
    }

    [Test]
    public void SimulateMoveTest()
    {
        ChessAI chessAI = new ChessAI(ChessColor.White, 2);
        ChessBoard board = new ChessBoard();
        foreach (var position in new List<(int, int)> {
            (1, 1), (2, 1), (0, 2), (1, 3), (1, 4), (0, 5), (2, 3)
        }) {
            board.SetPieceAt(position, new ChessPiece(ChessType.Pawn, ChessColor.Black));
        }
        board.SetPieceAt((0, 3), new ChessPiece(ChessType.Queen, ChessColor.White));
        board.PrintBoard();

        (int, int, int, int) move = (0, 3, 2, 1);
        ChessBoard newBoard = chessAI.SimulateMove(board, move);
        newBoard.PrintBoard();
        Assert.AreEqual(ChessType.Queen, newBoard.GetPieceAt(2, 1).type);
        Assert.AreEqual(ChessType.Queen, board.GetPieceAt(0, 3).type);
    }
}
