using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    // TODO public ChessPieceType Type;
    public void ChessPieceSelected() {
        Debug.Log("ChessPiece - ChessPieceSelected");
    }

    public void ChessPieceDeselected() {
        Debug.Log("ChessPiece - ChessPieceDeselected");
    }
}