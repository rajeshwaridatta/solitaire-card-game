using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class movesScript : MonoBehaviour {
	public Text moveText;
	public int moves ;
	// Use this for initialization

	void Start () {
		moves =0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
		{
			moves++;
		}
		moveText.text = "Moves : " + moves.ToString ();
	
	}
}
