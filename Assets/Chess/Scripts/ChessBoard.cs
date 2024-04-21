using UnityEngine;
using System.Collections.Generic;

public class ChessBoard
{
    private ChessPiece[,] board = new ChessPiece[8, 8];
    public ChessColor currentPlayer = ChessColor.White;

    public ChessPiece GetPieceAt(int x, int y)
    {
        return board[x, y];
    }

    public List<ChessPiece> GetPieces(ChessColor color)
    {
        List<ChessPiece> pieces = new List<ChessPiece>();
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                ChessPiece piece = GetPieceAt(x, y);
                if (piece != null && piece.color == color)
                {
                    pieces.Add(piece);
                }
            }
        }
        return pieces;
    }

    public void SetPieceAt((int, int) position, ChessPiece piece)
    {
        board[position.Item1, position.Item2] = piece;
        piece.SetPosition(position);
    }

    public bool IsWithinBounds(int x, int y)
    {
        return x >= 0 && x < 8 && y >= 0 && y < 8;
    }

    public void MovePiece((int, int) from, (int, int) to)
    {
        ChessPiece piece = board[from.Item1, from.Item2];
        RemovePiece(from);
        SetPieceAt(to, piece);
    }

    public void RemovePiece((int, int) position)
    {
        board[position.Item1, position.Item2] = null;
    }

    public void SetupBoard()
    {
        board = new ChessPiece[8, 8];
        SetPieceAt((0, 0), new ChessPiece(ChessType.Rook, ChessColor.Black, 0, 0));
        SetPieceAt((1, 0), new ChessPiece(ChessType.Knight, ChessColor.Black, 1, 0));
        SetPieceAt((2, 0), new ChessPiece(ChessType.Bishop, ChessColor.Black, 2, 0));
        SetPieceAt((3, 0), new ChessPiece(ChessType.Queen, ChessColor.Black, 3, 0));
        SetPieceAt((4, 0), new ChessPiece(ChessType.King, ChessColor.Black, 4, 0));
        SetPieceAt((5, 0), new ChessPiece(ChessType.Bishop, ChessColor.Black, 5, 0));
        SetPieceAt((6, 0), new ChessPiece(ChessType.Knight, ChessColor.Black, 6, 0));
        SetPieceAt((7, 0), new ChessPiece(ChessType.Rook, ChessColor.Black, 7, 0));
        for (int x = 0; x < 8; x++)
        {
            SetPieceAt((x, 1), new ChessPiece(ChessType.Pawn, ChessColor.Black, x, 1));
            SetPieceAt((x, 6), new ChessPiece(ChessType.Pawn, ChessColor.White, x, 6));
        }
        SetPieceAt((0, 7), new ChessPiece(ChessType.Rook, ChessColor.White, 0, 7));
        SetPieceAt((1, 7), new ChessPiece(ChessType.Knight, ChessColor.White, 1, 7));
        SetPieceAt((2, 7), new ChessPiece(ChessType.Bishop, ChessColor.White, 2, 7));
        SetPieceAt((3, 7), new ChessPiece(ChessType.Queen, ChessColor.White, 3, 7));
        SetPieceAt((4, 7), new ChessPiece(ChessType.King, ChessColor.White, 4, 7));
        SetPieceAt((5, 7), new ChessPiece(ChessType.Bishop, ChessColor.White, 5, 7));
        SetPieceAt((6, 7), new ChessPiece(ChessType.Knight, ChessColor.White, 6, 7));
        SetPieceAt((7, 7), new ChessPiece(ChessType.Rook, ChessColor.White, 7, 7));
    }

    public void PrintBoard()
    {
        string boardString = ".\t";
        for (int x = 0; x < 8; x++)
        {
            boardString += x + "\t";
        }
        boardString += "\n";
        for (int y = 0; y < 8; y++)
        {
            boardString += y + "\t";
            for (int x = 0; x < 8; x++)
            {
                ChessPiece piece = GetPieceAt(x, y);
                boardString += ((piece == null) ? "□" : piece.GetSymbol()) + "\t";
            }
            boardString += "\n";
        }
        Debug.Log(boardString);
    }

    public (int, int) GetScores()
    {
        int whiteScore = 0;
        int blackScore = 0;
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                ChessPiece piece = GetPieceAt(x, y);
                if (piece != null)
                {
                    if (piece.color == ChessColor.White)
                    {
                        whiteScore += piece.GetScore();
                    }
                    else
                    {
                        blackScore += piece.GetScore();
                    }
                }
            }
        }
        return (whiteScore, blackScore);
    }

    public ChessBoard Copy()
    {
        ChessBoard newBoard = new ChessBoard();
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                ChessPiece piece = GetPieceAt(x, y);
                if (piece != null)
                {
                    newBoard.SetPieceAt((x, y), piece.Copy());
                }
            }
        }
        return newBoard;
    }

    public List<ChessPiece> GetAdjacentPieces((int, int) position)
    {
        List<ChessPiece> adjacentPieces = new List<ChessPiece>();

        int[][] directions = {
            new int[] {-1, -1}, new int[] {0, -1}, new int[] {1, -1},
            new int[] {-1, 0}, new int[] {1, 0},
            new int[] {-1, 1}, new int[] {0, 1}, new int[] {1, 1}
        };

        foreach (var direction in directions)
        {
            int x = position.Item1 + direction[0];
            int y = position.Item2 + direction[1];

            if (IsWithinBounds(x, y))
            {
                ChessPiece piece = GetPieceAt(x, y);
                if (piece != null)
                {
                    adjacentPieces.Add(piece);
                }
            }
        }

        return adjacentPieces;
    }

}
