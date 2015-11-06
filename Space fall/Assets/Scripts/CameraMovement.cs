using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    
	private Transform   playerReference;
	private Rigidbody	playerPhysics;


	void Start() 
	{
		playerReference = GameObject.Find ("Player_reference").GetComponent<Transform> ();
		playerPhysics = GameObject.Find ("Player").GetComponent<Rigidbody> ();
	}

    void Update()
    {
        changePosition();
    }


    void changePosition()
    {	
		Vector3 playerPos = playerReference.position;
		float playerSpeed = playerPhysics.velocity.magnitude;
		if (playerSpeed == 0)
			playerSpeed = 1;
		Vector3 velocityDir = playerPhysics.velocity / playerSpeed;
		transform.position = playerPos - velocityDir * 100;
		transform.LookAt (playerPos, velocityDir);
		//Debug.DrawRay (playerPos,velocityDir * 1000 , Color.red);

	//	

	}
}
