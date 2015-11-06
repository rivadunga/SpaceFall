using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float aceleration = 0.1f;

	private Transform   playerBody;
	private Rigidbody   playerPhysics;


	void Start()
	{
		playerBody = GameObject.Find ("Player_body").GetComponent<Transform> ();
		playerPhysics = GetComponent<Rigidbody>();
	}

	void Update()
	{
		updateMovement ();
		changeDir();
	}

	Vector3 lastVel;
	void updateMovement()
	{
		Vector3 fwd = playerBody.TransformDirection (Vector3.up);
		Vector3 normal = playerBody.TransformDirection (-Vector3.forward);

		float GRAVITY = 3f;

		playerPhysics.AddForce(new Vector3(0,fwd.y,0)*2); 
		playerPhysics.AddForce(new Vector3(0,0,fwd.z)*1); 
		playerPhysics.AddForce(new Vector3(normal.x,0,0)*7); 
		playerPhysics.AddForce (Vector3.down * GRAVITY);

		Debug.DrawLine (playerBody.position, normal * 1000, Color.green);
		Debug.DrawLine (playerBody.position, fwd * 1000, Color.cyan);


		Vector3 aceleration = (playerPhysics.velocity - lastVel) / Time.deltaTime;
		Debug.Log (aceleration.x + " " + aceleration.y + " " + aceleration.z);
		lastVel = playerPhysics.velocity;
	}


	private void changeDir()
	{
		if (InputData.move_up)    playerBody.Rotate(Vector3.right * 2f);
		if (InputData.move_down)  playerBody.Rotate(Vector3.left * 2f);
		if (InputData.move_right) playerBody.Rotate (Vector3.up * 5f);
		if (InputData.move_left)  playerBody.Rotate(Vector3.down * 5f);	
	}


	bool maxSpeed = false;
	private void updateParticles()
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
