using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerInput : MonoBehaviour
{
    public GameObject      player;
    private PlayerMovement movement;

    void Start()
    {
        movement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (InputData.move_up) { movement.rotatePlayer(Vector3.right * 1f); }
        if (InputData.move_down) { movement.rotatePlayer(Vector3.left * 1f); }
        if (InputData.move_right) { movement.rotatePlayer(Vector3.up * 2f); }
        if (InputData.move_left) { movement.rotatePlayer(Vector3.down * 2f); }
    }
}
