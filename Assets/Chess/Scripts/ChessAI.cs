using System;
using System.Collections.Generic;
using UnityEngine;

public class ChessAI
{
    private ChessColor aiColor;
    private int maxDepth;

    public ChessAI(ChessColor aiColor, int maxDepth = 3)
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
        if (depth == 0 || IsTerminalNode(board))
        {
            return (Evaluate(board), -1, -1, -1, -1); // Evaluation function here
        }

        List<(int, int, int, int)> legalMoves = GenerateLegalMoves(board);

        if (maximizingPlayer)
        {
            int maxEval = int.MinValue;
            (int, int, int, int) bestMove = (-1, -1, -1, -1);

            foreach ((int fromX, int fromY, int toX, int toY) in legalMoves)
            {
                ChessBoard newBoard = SimulateMove(board, (fromX, fromY, toX, toY));
                int eval = Minimax(newBoard, depth - 1, alpha, beta, false).Item1;
                if (eval > maxEval)
                {
                    maxEval = eval;
                    bestMove = (fromX, fromY, toX, toY);
                }
                alpha = Math.Max(alpha, eval);
                if (beta <= alpha)
                    break;
            }

            return (maxEval, bestMove.Item1, bestMove.Item2, bestMove.Item3, bestMove.Item4);
        }
        else
        {
            int minEval = int.MaxValue;
            (int, int, int, int) bestMove = (-1, -1, -1, -1);

            foreach ((int fromX, int fromY, int toX, int toY) in legalMoves)
            {
                ChessBoard newBoard = SimulateMove(board, (fromX, fromY, toX, toY));
                int eval = Minimax(newBoard, depth - 1, alpha, beta, true).Item1;
                if (eval < minEval)
                {
                    minEval = eval;
                    bestMove = (fromX, fromY, toX, toY);
                }
                beta = Math.Min(beta, eval);
                if (beta <= alpha)
                    break;
            }

            return (minEval, bestMove.Item1, bestMove.Item2, bestMove.Item3, bestMove.Item4);
        }
    }

    private bool IsTerminalNode(ChessBoard board)
    {
        // Check if the game is in checkmate or stalemate
        bool isCheckmate = IsCheckmate(board);
        bool isStalemate = IsStalemate(board);

        // Return true if either condition is met
        return isCheckmate || isStalemate;
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

    public List<(int, int, int, int)> GenerateLegalMoves(ChessBoard board)
    {
        // Implement move generation logic
        List<(int, int, int, int)> legalMoves = new List<(int, int, int, int)>();
        // Sample implementation
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                ChessPiece piece = board.GetPieceAt(x, y);
                if (piece != null && piece.color == aiColor)
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

    private ChessBoard SimulateMove(ChessBoard board, (int, int, int, int) move)
    {
        ChessBoard newBoard = board.Copy();
        newBoard.MovePiece((move.Item1, move.Item2), (move.Item3, move.Item4));
        return newBoard;
    }

    private int Evaluate(ChessBoard board)
    {
        (int whiteScore, int blackScore) = board.GetScores();

        // Adjust the scores based on the AI's color
        int aiScore = (aiColor == ChessColor.White) ? whiteScore : blackScore;
        int opponentScore = (aiColor == ChessColor.White) ? blackScore : whiteScore;

        // Return the difference in scores as the evaluation
        return aiScore - opponentScore;
    }
}