using System.Collections;
using UnityEngine;

public class CameraMovementAI : MonoBehaviour
{
    private GameObject[] players;
    private Vector3      originalPos;
    private Quaternion   originalRot;
    private int          currentPlayer;

    void Start()
    {
        players     = GameObject.FindGameObjectsWithTag("player_ai");
        originalPos = transform.position;
        originalRot = transform.rotation;
        changeParent();
    }

    void Update()
    {
        changePosition();
    }

    void changePosition()
    {
        if (InputData.cam_down) { transform.Rotate(Vector3.left); }
        if (InputData.cam_up) { transform.Rotate(Vector3.right); }
        if (InputData.cam_left) { transform.Rotate(Vector3.up); }
        if (InputData.cam_right) { transform.Rotate(Vector3.down); }
        if (InputData.shot_down)
        {
            currentPlayer = (currentPlayer + 1) % players.Length;
            changeParent();
        }

        if (InputData.move_down) { transform.Translate(Vector3.down); }
        if (InputData.move_up) { transform.Translate(Vector3.up); }
        if (InputData.move_left) { transform.Translate(Vector3.left); }
        if (InputData.move_right) { transform.Translate(Vector3.right); }
    }

    void changeParent()
    {
        for (int i = 0; i < players.Length; i++)
        {
            AI ai = players[i].GetComponent<AI>();
            ai.showEdges = currentPlayer == i;
            ai.showNodes = currentPlayer == i;
            ai.showPath  = currentPlayer == i;
        }
        transform.parent        = players[currentPlayer].transform;
        transform.localPosition = originalPos;
        transform.localRotation = originalRot;
    }
}
