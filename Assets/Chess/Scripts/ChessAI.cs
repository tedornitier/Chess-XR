using System;
using System.Collections.Generic;
using UnityEngine;

public class ChessAI
{
    public ChessColor aiColor;
    private int maxDepth;

    public ChessAI(ChessColor aiColor, int maxDepth)
    {
        this.aiColor = aiColor;
        this.maxDepth = maxDepth;
    }

    public (int, int, int, int) GetBestMove(ChessBoard board)
    {
        (int _, int fromX, int fromY, int toX, int toY) = Minimax(board, maxDepth, int.MinValue, int.MaxValue, true);
        return (fromX, fromY, toX, toY);
    }

    private (int, int, int, int, int) Minimax(ChessBoard board, int depth, int alpha, int beta, bool maximizingPlayer)
    {
        ChessColor currentPlayerColor = maximizingPlayer ? aiColor : (aiColor == ChessColor.White ? ChessColor.Black : ChessColor.White);

        if (depth == 0 || IsTerminalNode(board))
        {
            return (Evaluate(board), -1, -1, -1, -1);
        }

        List<(int, int, int, int)> legalMoves = GenerateLegalMoves(board, currentPlayerColor);

        int bestEval = maximizingPlayer ? int.MinValue : int.MaxValue;
        (int, int, int, int) bestMove = (-1, -1, -1, -1);

        foreach ((int fromX, int fromY, int toX, int toY) in legalMoves)
        {
            ChessBoard newBoard = SimulateMove(board, (fromX, fromY, toX, toY));
            int eval = Minimax(newBoard, depth - 1, alpha, beta, !maximizingPlayer).Item1;

            if ((maximizingPlayer && eval > bestEval) || (!maximizingPlayer && eval < bestEval))
            {
                bestEval = eval;
                bestMove = (fromX, fromY, toX, toY);
            }

            if (maximizingPlayer)
            {
                alpha = Math.Max(alpha, eval);
                if (beta <= alpha) break;
            }
            else
            {
                beta = Math.Min(beta, eval);
                if (beta <= alpha) break;
            }
        }

        return (bestEval, bestMove.Item1, bestMove.Item2, bestMove.Item3, bestMove.Item4);
    }

    private bool IsTerminalNode(ChessBoard board)
    {
        return IsCheckmate(board) || IsStalemate(board);
    }

    private bool IsCheckmate(ChessBoard board)
    {
        // TODO Implement checkmate detection logic
        return false;
    }

    private bool IsStalemate(ChessBoard board)
    {
        // TODO Implement stalemate detection logic
        return false;
    }

    public List<(int, int, int, int)> GenerateLegalMoves(ChessBoard board, ChessColor currentPlayerColor)
    {
        List<(int, int, int, int)> legalMoves = new List<(int, int, int, int)>();
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                ChessPiece piece = board.GetPieceAt(x, y);
                if (piece != null && piece.color == currentPlayerColor)
                {
                    HashSet<(int, int)> possibleMoves = piece.GetPossibleMoves(board);
                    foreach ((int, int) move in possibleMoves)
                    {
                        legalMoves.Add((x, y, move.Item1, move.Item2));
                    }
                }
            }
        }
        return legalMoves;
    }

    // TODO private
    public ChessBoard SimulateMove(ChessBoard board, (int, int, int, int) move)
    {
        ChessBoard newBoard = board.Copy();
        newBoard.MovePiece((move.Item1, move.Item2), (move.Item3, move.Item4));
        return newBoard;
    }

    // TODO private
    public int Evaluate(ChessBoard board)
    {
        (int whiteScore, int blackScore) = board.GetScores();

        int aiScore = (aiColor == ChessColor.White) ? whiteScore : blackScore;
        int opponentScore = (aiColor == ChessColor.White) ? blackScore : whiteScore;

        return aiScore - opponentScore;
    }

    public int GetPawnChainLength(ChessBoard board, ChessPiece pawn)
    {
        // Recursively find the length of the pawn chain starting from the specified pawn
        int chainLength = 1; // Pawn itself is part of the chain

        // Add the current pawn to the visited set
        visitedPawns.Add(pawn);

        // Get neighboring pawns of the same color
        List<ChessPiece> neighbors = board.GetAdjacentPieces((pawn.column, pawn.row));
        neighbors = neighbors.FindAll(neighbor => neighbor != null && neighbor.color == pawn.color && neighbor.type == ChessType.Pawn);

        // Iterate over neighboring pawns and recursively find chain length
        foreach (var neighbor in neighbors)
        {
            // Only visit the neighbor if it hasn't been visited yet
            if (!visitedPawns.Contains(neighbor))
                chainLength += GetPawnChainLength(board, neighbor);
    }

    private bool IsCheck(ChessBoard board)
    {
        // Find the current player's king
        ChessPiece king = board.GetPieces(board.currentPlayer).Find(piece => piece.type == ChessType.King);

        // Check if the king is under attack by any enemy piece
        foreach (var piece in board.GetPieces(board.currentPlayer == ChessColor.White ? ChessColor.Black : ChessColor.White))
        {
            if (piece.GetPossibleMoves(board).Contains((king.column, king.row)))
            {
                return true; // King is under attack
            }
        }

        return false; // King is not under attack
    }
}