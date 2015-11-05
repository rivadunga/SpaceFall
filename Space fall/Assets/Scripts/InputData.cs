using UnityEngine;
using System.Collections;

public class InputData : MonoBehaviour {

	public static bool move_up = false;
	public static bool move_down = false;
	public static bool move_left = false;
	public static bool move_right = false;


	void Update () 
	{
		updateControls ();
	}

	private void updateControls()
	{
		move_up    = Input.GetKey (KeyCode.UpArrow);
		move_down  = Input.GetKey (KeyCode.DownArrow);
		move_left  = Input.GetKey (KeyCode.LeftArrow);
		move_right = Input.GetKey (KeyCode.RightArrow);
	}
}
