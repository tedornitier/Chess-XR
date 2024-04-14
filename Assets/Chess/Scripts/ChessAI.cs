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
}