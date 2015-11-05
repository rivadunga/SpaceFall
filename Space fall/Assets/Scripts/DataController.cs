using UnityEngine;
using System.Collections;

public class DataController : MonoBehaviour {

	LevelData data;

	void Start()
	{
		data = gameObject.GetComponent<LevelData>();
	}

	void Update () 
	{
		updateTime ();
	}

	public void addScore(int score)
	{
		data.setScore (data.getScore () + score);
	}

	private void updateTime()
	{
		data.setTime(data.getTime() + Time.deltaTime);
	}

}
