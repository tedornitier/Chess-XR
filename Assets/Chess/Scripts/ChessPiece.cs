using UnityEngine;
using System.Collections.Generic;

public class ChessPiece : MonoBehaviour
{
    [SerializeField]
    public ChessType type;
    [SerializeField]
    public ChessColor color;

    public void ChessPieceSelected() {
        Debug.Log("ChessPiece - ChessPieceSelected");
    }

    public void ChessPieceDeselected() {
        Debug.Log("ChessPiece - ChessPieceDeselected");
    }
}