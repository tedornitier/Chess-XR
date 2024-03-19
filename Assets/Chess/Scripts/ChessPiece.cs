using UnityEngine;
using System.Collections.Generic;

public class ChessPiece
{
    [SerializeField]
    public ChessType type;
    [SerializeField]
    public ChessColor color;

    int column = -1;
    int row = -1;

    public ChessPiece(ChessType type, ChessColor color) {
        this.type = type;
        this.color = color;
    }

    public ChessPiece(ChessType type, ChessColor color, int column, int row) {
        this.type = type;
        this.color = color;
        this.column = column;
        this.row = row;
    }

    public void SetPosition(int x, int y) {
        column = x;
        row = y;
    }

    public string GetSymbol() {
        switch (type) {
            case ChessType.King:
                if (color == ChessColor.Black) {
                    return "♔";
                } else {
                    return "♚";
                }
                break;
            case ChessType.Queen:
                if (color == ChessColor.Black) {
                    return "♕";
                } else {
                    return "♛";
                }
                break;
            case ChessType.Rook:
                if (color == ChessColor.Black) {
                    return "♖";
                } else {
                    return "♜";
                }
                break;
            case ChessType.Bishop:
                if (color == ChessColor.Black) {
                    return "♗";
                } else {
                    return "♝";
                }
                break;
            case ChessType.Knight:
                if (color == ChessColor.Black) {
                    return "♘";
                } else {
                    return "♞";
                }
                break;
            case ChessType.Pawn:
                if (color == ChessColor.Black) {
                    return "♙";
                } else {
                    return "♟";
                }
                break;
            default:
                break;
        }
        return "";
    }

    public void MoveTo(int x, int y) {
        column = x;
        row = y;
    }
}