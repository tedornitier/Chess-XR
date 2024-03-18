using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    [SerializeField] GameObject playArea;
    float playAreaLength;

    void Start()
    {
        playAreaLength = playArea.transform.localScale.x * playArea.GetComponent<MeshFilter>().mesh.bounds.size.x;
        Debug.Log("ChessBoard - Play Area Length: " + playAreaLength);
    }
}
