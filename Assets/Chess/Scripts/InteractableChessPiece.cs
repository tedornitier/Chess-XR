using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableChessPiece : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void onPiecePickUp()
    {
        Debug.Log("onPiecePickUp");
        GameObject.Find("Game").GetComponent<Game>().onPiecePickUp(transform.localPosition);
    }

    public void onPieceDrop()
    {
        Debug.Log("onPieceDrop");
        GameObject.Find("Game").GetComponent<Game>().onPieceDrop(transform.localPosition);
    }
}
