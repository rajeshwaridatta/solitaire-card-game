using UnityEngine;
using System.Collections;
using System;

public class Card : MonoBehaviour
{
	public const float WIDTH = 3.28f;
	public const float HEIGHT = 5.01f;

	public SpriteRenderer[] signSprites;
	public TextMesh[] textMesh;

	private Sprite sign;
	public Sprite slotsign;
	private string textSign;
	public Color textColor;
	private int colorValue;
	public int faceValue;
	public int suitValue;
	private Vector3 targetPosition;

	public Sprite displaySprite;

	public void SetPosition(Vector3 targetPosition) {
		this.targetPosition = targetPosition;
	}

	public Vector3 GetPosition() {
		return this.transform.position;
	}


	void Start ()
	{
		assigningPropertiesToCard ();
	}

	void Update ()
	{
		if (targetPosition != this.transform.position) {
			// Do animation here
			this.transform.position = targetPosition;
		}
	}

	public void AssignSignName(Sprite sign) 
	{
		UpdateSuit (sign);
	}

	public void AssignSign(Sprite signSprite,string textSign, Color textColor)
	{
		UpdateSuit (signSprite);
		this.textSign = textSign;
		this.textColor = textColor;

		for (int i = 0; i < signSprites.Length; i++)
		{
			signSprites [i].sprite = sign;
		}

		for (int i = 0; i < textMesh.Length; i++)
		{
			textMesh [i].text = textSign;
			textMesh [i].color = textColor;
		}
	}
	public int getFaceValue()
	{
		return this.faceValue;
	}

	public int  getColorValue()
	{
		return this.colorValue;
	}
	public int getSuitValue()
	{
		return this.suitValue;
	}

	void assigningPropertiesToCard()
	{
		Color redColor = Color.red;
		Color blackColor = Color.black;

		if (textColor == redColor) {
			colorValue = 1;
		} else if (textColor == blackColor) {
			colorValue = 2;
		}
		if (textSign == "A") {
			faceValue = 1;
		}
		if (textSign == "2") {
			faceValue = 2;
		}
		if (textSign == "3") {
			faceValue = 3;
		}
		if (textSign == "4") {
			faceValue = 4;
		}
		if (textSign == "5") {
			faceValue = 5;
		}
		if (textSign == "6") {
			faceValue = 6;
		}
		if (textSign == "7") {
			faceValue = 7;
		}
		if (textSign == "8") {
			faceValue = 8;
		}
		if (textSign == "9") {
			faceValue = 9;
		}
		if (textSign == "10") {
			faceValue = 10;
		}
		if (textSign == "J") {
			faceValue = 11;
		}
		if (textSign == "Q") {
			faceValue = 12;
		}
		if (textSign == "K") {
			faceValue = 13;
		}
		// fix suit values
	}

	public void Flip () {
		this.transform.Rotate (Vector3.right * 180);
	}

	private void UpdateSuit(Sprite sign) {
		this.sign = sign;
		switch (sign.name) {
		case "Club_Card":
			suitValue = 1;
			break;
		case "Diamond_Card":
			suitValue = 2;
			break;
		case "Spade_Card":
			suitValue = 3;
			break;
		case "Heart_Card":
			suitValue = 4;
			break;
		}
	}
}

