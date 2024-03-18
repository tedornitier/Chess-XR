using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine.Networking;

public class Chess : MonoBehaviour
{
    float L = 22.44f; // length of the board
    int lastX = 0;
    int lastZ = 0;

    void Start()
    {
        (lastX, lastZ) = CalculateCellPosition(transform.localPosition);
    }

    void Update()
    {

    }

    public void ChessPieceSelected() {
        Debug.Log("ChessPieceSelected");
    }

    public void ChessPieceDeselected() {
        Debug.Log("ChessPieceDeselected");
    }

    public void ChessPieceMoved() {
        (int x, int z) = CalculateCellPosition(transform.localPosition);
        Debug.Log($"ChessPieceMoved x: {x}, z: {z}, position: {transform.localPosition}");
        (float positionX, float positionZ) = CalculatePosition(x, z);
        if (positionX < -L / 2 || positionX > L / 2 || positionZ < -L / 2 || positionZ > L / 2) {
            Debug.Log("Out of board");
            return;
        }

        string id = gameObject.name;
        (lastX, lastZ) = (x, z);
    }

    public (int, int) CalculateCellPosition(Vector3 position) {
        int x = (int) Math.Floor(8 * position.x / L) + 4;
        int z = (int) Math.Floor(8 * position.z / L) + 4;
        return (x, z);
    }

    public (float, float) CalculatePosition(int x, int z) {
        float positionX = (L * (x - 4) / 8 + L * (x - 4 + 1) / 8) / 2;
        float positionZ = (L * (z - 4) / 8 + L * (z - 4 + 1) / 8) / 2;
        return (positionX, positionZ);
    }
}