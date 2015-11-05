using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelController : MonoBehaviour {

	//Class developed to handle the initial story

	GameObject aux_model;
	GameObject story_text;
	GameObject story_image;
	GameObject story_camera;
	GameObject story_camera_2;

	void Start () {
		story_text = GameObject.Find ("lvl_story_text");
		story_image = GameObject.Find ("lvl_story_image");
		aux_model = GameObject.Find ("lvl_aux_model");
		story_camera = GameObject.Find ("lvl_camera");
		story_camera_2 = GameObject.Find ("lvl_camera_2");
		aux_model.SetActive (false);
		story_text.SetActive (false);
		story_image.SetActive (false);
		story_camera_2.SetActive (false);
	}

	bool story0 = true;
	bool story1 = true;
	bool story2 = true;
	bool modelWalk = false;
	bool story3 = true;

	void Update () {


		if (Time.time > 3 && story0) 
		{
			story_text.SetActive (true);
			story_image.SetActive (true);
			story_text.GetComponent<Text>().text = "¿Estas listo?";
			story0 = false;
		}


		if (Time.time > 5 && story1) 
		{
			story_text.GetComponent<Text>().text = "¡Adelante!";
			story1 = false;
		}

		if (Time.time > 7 && story2) 
		{
			story_text.SetActive(false);
			story_image.SetActive(false);
			aux_model.gameObject.SetActive(true);
			story_camera.SetActive(false);
			story_camera_2.SetActive(true);
			modelWalk = true;
			story2 = false;
			//TransformGameObject.Find("lvl_story_image").transform;

		}

		if (modelWalk) {
			aux_model.transform.position = aux_model.transform.position + (Vector3.Lerp(Vector3.forward,Vector3.right,0.4f)) * 0.5f;
		}



		if (Time.time > 10f && story3){
			modelWalk = false;
			story3 = false;
			story_camera_2.SetActive(false);

		}
	}
}
