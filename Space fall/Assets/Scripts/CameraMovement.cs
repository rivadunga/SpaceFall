using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {


	Vector3 difference;

	void Start(){
		//Initialize the initial reference between th3 player and the camera
		difference = transform.position - 
			GameObject.Find ("Player_reference").GetComponent<Transform> ().position;
	}

	void Update () {
		changePosition ();
	}

	void changePosition()
	{
	
		Vector3 playerPosition 
			= GameObject.Find("Player_reference").GetComponent<Transform>().position;
		Vector3 fwd = GameObject.Find("Player_reference").GetComponent<Transform>()
			.TransformDirection (-Vector3.forward);

		//Set the currrent position of the camara
		transform.position = playerPosition + difference;

		//TODO check camera bug
		if (fwd.z > -0.1f && fwd.z < 0.8f) {
			Debug.DrawLine (playerPosition, fwd * 1000, Color.red);
			//Update the rotation
			transform.LookAt(playerPosition,fwd);
		}

	}
}
