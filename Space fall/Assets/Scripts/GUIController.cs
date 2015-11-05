using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour {

	private Text score;
	private LevelData data;

	void Start () 
	{
		score = GameObject.Find("gui_score").GetComponent<Text>();
		data = GetComponent<LevelData> ();
	}
	
	void Update () {
		updateElements ();
	}

	void updateElements()
	{
		score.text = data.getScore () + "";
	}
}
