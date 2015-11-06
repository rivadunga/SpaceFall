using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerCollision : MonoBehaviour {

	private DataController data;

	void Start()
	{
		data = GameObject.Find("lvl_controller").GetComponent<DataController>();
	}


	void OnTriggerEnter(Collider col) {
		if (col.name == "obj_point") {
			data.addScore(1000);
		}
	}

}
