using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardTextScript : MonoBehaviour {
	public Text moveText;
	public GameObject winPanel ;
	public int moves =0;


	// Use this for initialization
	void Start () 
	{
		moveText.text = "Moves" + moves+ "kl";
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown (0)) 
		{
			moves++;
		}
		moveText.text = "Moves" + moves + "fsdfsef";

	}
}
