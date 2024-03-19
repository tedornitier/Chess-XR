using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    [SerializeField]
    public ChessType type;

    public void ChessPieceSelected() {
        Debug.Log("ChessPiece - ChessPieceSelected");
    }

    public void ChessPieceDeselected() {
        Debug.Log("ChessPiece - ChessPieceDeselected");
    }
}