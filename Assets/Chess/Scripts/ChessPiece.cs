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

    public HashSet<(int, int)> GetPossibleMoves(ChessBoard board) {
        HashSet<(int, int)> possibleMoves = new HashSet<(int, int)>();

        switch (type) {
            case ChessType.King:
                (int, int)[] kingMoves = new (int, int)[] {
                    (-1, -1), (-1, 0), (-1, 1), (0, -1),
                    (0, 1), (1, -1), (1, 0), (1, 1)
                };
                foreach (var move in kingMoves) {
                    int newX = column + move.Item1;
                    int newY = row + move.Item2;
                    if (ChessBoard.IsWithinBounds(newX, newY)) {
                        ChessPiece targetPiece = board.GetPieceAt(newX, newY);
                        if (targetPiece == null || targetPiece.color != color) {
                            possibleMoves.Add((newX, newY));
                        }
                    }
                }
                break;
            case ChessType.Queen:
                (int, int)[] queenDirections = new (int, int)[] {
                    (-1, -1), (-1, 0), (-1, 1), (0, -1),
                    (0, 1), (1, -1), (1, 0), (1, 1)
                };
                for (int i = 0; i < queenDirections.GetLength(0); i++) {
                    GetMovesInDirection(board, possibleMoves, queenDirections[i].Item1, queenDirections[i].Item2);
                }
                break;
            case ChessType.Rook:
                (int, int)[] rookDirections = new (int, int)[] {
                    (-1, 0), (0, -1), (0, 1), (1, 0)
                };
                for (int i = 0; i < rookDirections.GetLength(0); i++) {
                    GetMovesInDirection(board, possibleMoves, rookDirections[i].Item1, rookDirections[i].Item2);
                }
                break;
            case ChessType.Bishop:
                (int, int)[] bishopDirections = new (int, int)[] {
                    (-1, -1), (-1, 1), (1, -1), (1, 1)
                };
                for (int i = 0; i < bishopDirections.GetLength(0); i++) {
                    GetMovesInDirection(board, possibleMoves, bishopDirections[i].Item1, bishopDirections[i].Item2);
                }
                break;
            case ChessType.Knight:
                (int, int)[] knightMoves = new (int, int)[] {
                    (-2, -1), (-2, 1), (-1, -2), (-1, 2),
                    (1, -2), (1, 2), (2, -1), (2, 1)
                };
                foreach (var move in knightMoves) {
                    int newX = column + move.Item1;
                    int newY = row + move.Item2;
                    if (ChessBoard.IsWithinBounds(newX, newY)) {
                        ChessPiece targetPiece = board.GetPieceAt(newX, newY);
                        if (targetPiece == null || targetPiece.color != color) {
                            possibleMoves.Add((newX, newY));
                        }
                    }
                }
                break;
            case ChessType.Pawn:
                int direction = (color == ChessColor.White) ? -1 : 1;
                int initialRow = (color == ChessColor.White) ? 6 : 1;
                int nextRow = row + direction;
                int nextNextRow = row + 2 * direction;

                if (ChessBoard.IsWithinBounds(column, nextRow)) {
                    if (board.GetPieceAt(column, nextRow) == null) {
                        possibleMoves.Add((column, nextRow));

                        if (row == initialRow && ChessBoard.IsWithinBounds(column, nextNextRow) && board.GetPieceAt(column, nextNextRow) == null) {
                            possibleMoves.Add((column, nextNextRow));
                        }
                    }

                    int[] captureCols = { column - 1, column + 1 };
                    foreach (int col in captureCols) {
                        if (ChessBoard.IsWithinBounds(col, nextRow)) {
                            ChessPiece targetPiece = board.GetPieceAt(col, nextRow);
                            if (targetPiece != null && targetPiece.color != color) {
                                possibleMoves.Add((col, nextRow));
                            }
                        }
                    }
                }

                // TODO en passant
                break;
            default:
                Debug.LogError("Unknown chess piece type: " + type);
                break;
        }

        return possibleMoves;
    }

    private void GetMovesInDirection(ChessBoard board, HashSet<(int, int)> possibleMoves, int dx, int dy) {
        int x = column + dx;
        int y = row + dy;

        while (ChessBoard.IsWithinBounds(x, y)) {
            ChessPiece targetPiece = board.GetPieceAt(x, y);

            // Check if the target square is empty or has an opponent's piece
            if (targetPiece == null || targetPiece.color != color) {
                possibleMoves.Add((x, y));
            }

            // If the target square is not empty, stop further movement in this direction
            if (targetPiece != null) {
                break;
            }

            // Move further in the same direction
            x += dx;
            y += dy;
        }
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
}