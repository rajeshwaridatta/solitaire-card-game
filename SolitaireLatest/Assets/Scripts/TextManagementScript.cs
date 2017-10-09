using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextManagementScript : MonoBehaviour {

	public Text timeText;
	int counter = 0;
	int sec = 0;
	int min = 0;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		setTimer ();
	
	}

	void setTimer()
	{
		

		counter++;
		if (counter == 30) 
		{
			sec++;
			counter = 0;
		}
		if (sec < 10) {

			sec = int.Parse ("0" + Mathf.RoundToInt(sec).ToString());
		}

		if (sec > 60) {
			min++;
			sec = 0;
		}
		timeText.text = "Time taken : " + min + ":" + sec;
	}
}
