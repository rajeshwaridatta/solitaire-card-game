using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RuleEngine : MonoBehaviour {
	int totalNumOfCardsPerSuit;


	void Start () {
		totalNumOfCardsPerSuit = 2;
	}
	

	void Update () {}

	public bool isMatching(Card selectedCard, Card baseCard)
	{
		if ((baseCard.getFaceValue () - selectedCard.getFaceValue () == 1) && 
			((baseCard.getColorValue() + selectedCard.getColorValue()) == 3)) {
			//Debug.Log ("yeeeee" + (baseCard.getFaceValue () - selectedCard.getFaceValue ()) +" diff");
			return true;
		}
		//Debug.Log (baseCard.getFaceValue () + "face"+ selectedCard.getFaceValue ());
		//Debug.Log (baseCard.getColorValue () + "color"+ selectedCard.getColorValue ());
		return false;
	}

    public bool isPlaceHolderCardMatching(Card selectedCard, Card baseCard) 
	{
		if ((selectedCard.getFaceValue () - baseCard.getFaceValue () == 1) && 
		    selectedCard.getSuitValue () == baseCard.getSuitValue ()) {
			return true;
		}
		return false;
	}

	public bool allStacksAreFull(List<PlaceholderColumn> columns)
	{
		foreach (PlaceholderColumn column in columns) 
		{
			if (column.getStackLength () != totalNumOfCardsPerSuit) {
				return false;
			}
		}
		return true;
	}

}