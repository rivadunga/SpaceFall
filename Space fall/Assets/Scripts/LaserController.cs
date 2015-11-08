using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour {

	public 	Transform playerLaserRef;

	private LineRenderer laser;


	void Start () {
		laser = GetComponent<LineRenderer>();
		laser.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (InputData.shot_down){
			StopCoroutine("fireLaser");
			StartCoroutine("fireLaser");
		}
	}

	IEnumerator fireLaser()
	{
		laser.enabled = true;
		Vector3 currentPosition = playerLaserRef.position;

		while (InputData.shot_pressed){

			laser.material.mainTextureOffset = new Vector2(0,Time.time);

			Ray ray = new Ray(playerLaserRef.position, playerLaserRef.forward);
			RaycastHit hit;

			laser.SetPosition(0,ray.origin);
			Vector3 newPosition;
			if (Physics.Raycast(ray,out hit, 150))
				newPosition = Vector3.Lerp(currentPosition, hit.point, Time.deltaTime*3);
			 else
				newPosition = Vector3.Lerp(currentPosition, ray.GetPoint(150), Time.deltaTime*3);
			
			laser.SetPosition(1,newPosition);
			currentPosition = newPosition;

			yield return null;
		}
		laser.enabled = false;
	}
}
