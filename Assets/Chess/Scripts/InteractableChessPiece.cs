using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableChessPiece : MonoBehaviour
{
    [SerializeField]
    bool pickUp = false;
    [SerializeField]
    bool drop = false;

    void Start()
    {

    }

    void Update()
    {
        if (pickUp)
        {
            onPiecePickUp(true);
            pickUp = false;
        }
        if (drop)
        {
            onPieceDrop(true);
            drop = false;
        }
    }

    public void onPiecePickUp(bool debug = false)
    {
        Debug.Log("onPiecePickUp");
        GameObject.Find("Game").GetComponent<Game>().onPiecePickUp(transform.localPosition, debug);
    }

    public void onPieceDrop(bool debug = false)
    {
        Debug.Log("onPieceDrop");
        GameObject.Find("Game").GetComponent<Game>().onPieceDrop(transform.localPosition, debug);
    }
}
