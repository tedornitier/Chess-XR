using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class ChessPieceTests : MonoBehaviour
{
    [Test]
    public void GetPawnPossibleMovesTest()
    {
        /*0 1 2 3 4 5 6 7
        0 □ □ □ □ □ □ □ □
        1[♙]□ □ □ □ □ □ □
        2 x □ □ □ □ □ □ □
        3 x □ □ □ □ □ □ □
        4 □ ♕ □ □ □ □ □ □
        5 □ ♛ □ □ □ □ □ □
        6 ♖ □ □ □ □ □ □ □
        7 □ □ □ □ □ □ □ □*/
        ChessPiece piece = new ChessPiece(ChessType.Pawn, ChessColor.Black);
        ChessBoard board = new ChessBoard();
        board.SetPieceAt(0, 1, piece);
        board.SetPieceAt(1, 4, new ChessPiece(ChessType.Queen, ChessColor.Black));
        board.SetPieceAt(1, 5, new ChessPiece(ChessType.Queen, ChessColor.White));
        board.SetPieceAt(0, 6, new ChessPiece(ChessType.Rook, ChessColor.Black));
        CollectionAssert.AreEquivalent(new HashSet<(int, int)> {
            (0, 2), (0, 3)
        }, piece.GetPossibleMoves(board));
        board.PrintBoard();

        /*0 1 2 3 4 5 6 7
        0 □ □ □ □ □ □ □ □
        1 □ □ □ □ □ □ □ □
        2 □ □ □ □ □ □ □ □
        3[♙]□ □ □ □ □ □ □
        4 x ♕ □ □ □ □ □ □
        5 □ ♛ □ □ □ □ □ □
        6 ♖ □ □ □ □ □ □ □
        7 □ □ □ □ □ □ □ □*/
        board.MovePiece(0, 1, 0, 3);
        CollectionAssert.AreEquivalent(new HashSet<(int, int)> {
            (0, 4)
        }, piece.GetPossibleMoves(board));
        board.PrintBoard();

        /*0 1 2 3 4 5 6 7
        0 □ □ □ □ □ □ □ □
        1 □ □ □ □ □ □ □ □
        2 □ □ □ □ □ □ □ □
        3 □ □ □ □ □ □ □ □
        4[♙]♕ □ □ □ □ □ □
        5 x x □ □ □ □ □ □
        6 ♖ □ □ □ □ □ □ □
        7 □ □ □ □ □ □ □ □*/
        board.MovePiece(0, 3, 0, 4);
        CollectionAssert.AreEquivalent(new HashSet<(int, int)> {
            (0, 5), (1, 5)
        }, piece.GetPossibleMoves(board));
        board.PrintBoard();

        /*0 1 2 3 4 5 6 7
        0 □ □ □ □ □ □ □ □
        1 □ □ □ □ □ □ □ □
        2 □ □ □ □ □ □ □ □
        3 □ □ □ □ □ □ □ □
        4 □ ♕ □ □ □ □ □ □
        5[♙]□ □ □ □ □ □ □
        6 ♖ □ □ □ □ □ □ □
        7 □ □ □ □ □ □ □ □*/
        board.MovePiece(0, 4, 0, 5);
        Assert.AreEqual(0, piece.GetPossibleMoves(board).Count);
        board.PrintBoard();

        /*0 1 2 3 4 5 6 7
        0 □ □ □ □ □ □ □ □
        1 □ □ □ □ □ □ □ □
        2 □ □ □ □ □ □ □ □
        3 □ □ □ □ □ □ □ □
        4 □ ♕ □ □ □ □ □ □
        5 □ □ □ □ □ □ □ □
        6[♙]□ □ □ □ □ □ ♖
        7 x □ □ □ □ □ □ □*/
        board.MovePiece(0, 6, 7, 6);
        board.MovePiece(0, 5, 0, 6);
        CollectionAssert.AreEquivalent(new HashSet<(int, int)> {
            (0, 7)
        }, piece.GetPossibleMoves(board));
        board.PrintBoard();

        /*0 1 2 3 4 5 6 7
        0 □ □ □ □ □ □ □ □
        1 □ □ □ □ □ □ □ □
        2 □ □ □ □ □ □ □ □
        3 □ □ □ □ □ □ □ □
        4 □ ♕ □ □ □ □ □ □
        5 □ □ □ □ □ □ □ □
        6 □ □ □ □ □ □ □ ♖
        7[♙]□ □ □ □ □ □ □*/
        board.MovePiece(0, 6, 0, 7);
        Assert.AreEqual(0, piece.GetPossibleMoves(board).Count);
        board.PrintBoard();
    }
}
