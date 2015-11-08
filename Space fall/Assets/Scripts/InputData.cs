using UnityEngine;
using System.Collections;

public class InputData : MonoBehaviour
{
    public static bool move_up      = false;
    public static bool move_down    = false;
    public static bool move_left    = false;
    public static bool move_right   = false;
    public static bool shot_pressed = false;
    public static bool shot_down    = false;
    public static bool cam_up       = false;
    public static bool cam_down     = false;
    public static bool cam_left     = false;
    public static bool cam_right    = false;

    void Update()
    {
        updateControls();
    }

    private void updateControls()
    {
        move_up      = Input.GetKey(KeyCode.W);
        move_down    = Input.GetKey(KeyCode.S);
        move_left    = Input.GetKey(KeyCode.A);
        move_right   = Input.GetKey(KeyCode.D);
        shot_down    = Input.GetKeyDown(KeyCode.Space);
        shot_pressed = Input.GetKey(KeyCode.Space);
        cam_up       = Input.GetKey(KeyCode.UpArrow);
        cam_down     = Input.GetKey(KeyCode.DownArrow);
        cam_left     = Input.GetKey(KeyCode.LeftArrow);
        cam_right    = Input.GetKey(KeyCode.RightArrow);
    }
}
