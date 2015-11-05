using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	PlayerAudio audio;

	void Update()
	{
		audio = GetComponent<PlayerAudio>();
		changeDir();
		updateMovement ();
	}

	void updateMovement()
	{
		Vector3 velocity = GetComponent<Rigidbody> ().velocity;
		Vector3 fwd = transform.TransformDirection (-Vector3.forward);
		Debug.DrawLine (transform.position, fwd * 1000,Color.cyan);

		//IMPORTANT the player velocity is the result of the sum of the
		//velocity that imitates the gravity (Vector3.down) and the 
		//fwd vector that represents the normal force of the player 
		//with the air

		Vector3 vel = (fwd + Vector3.down*1.2f) * 6f;
		GetComponent<Rigidbody> ().velocity = vel;
		updateParticles ();
	}


	void changeDir()
	{
		if (InputData.move_up)    GetComponent<Transform>().Rotate(Vector3.right*0.5f);
		if (InputData.move_down)  GetComponent<Transform>().Rotate(Vector3.left*0.5f);
		if (InputData.move_right) GetComponent<Transform> ().Rotate (Vector3.up * 0.5f);
		if (InputData.move_left)  GetComponent<Transform>().Rotate(Vector3.down*0.5f);	
	}


	bool maxSpeed = false;
	void updateParticles()
	{
		if (GetComponent<Rigidbody> ().velocity.z > 3 && !maxSpeed) {
			GameObject.Find ("Player_effect").GetComponent<ParticleSystem>().emissionRate = 1000;
			GameObject.Find ("Player_effect").GetComponent<ParticleSystem>().startColor = new Color(1,1,1,0.03f);
			GameObject.Find ("Player_effect").GetComponent<ParticleSystem>().startSpeed = GetComponent<Rigidbody>().velocity.z*20f;
			maxSpeed = true;
		} else {
			GameObject.Find ("Player_effect").GetComponent<ParticleSystem>().emissionRate = 20;
			GameObject.Find ("Player_effect").GetComponent<ParticleSystem>().startColor = new Color(1,1,1,0.005f);
			GameObject.Find ("Player_effect").GetComponent<ParticleSystem>().startSpeed = GetComponent<Rigidbody>().velocity.z*2f;
			maxSpeed = false;
		}
	}
	
}
