using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public static GameObject DraggedInstance;

	Vector3 _startPosition;
	Vector3 _offsetToMouse;
	float _zDistanceToCamera;
	float maxZHeight = 3;
	private BaseColumn columnSelected;
	Vector3 endPosition; // it has to be total nmber of the cards * their z distances
	public GameObject cardManager;
	CardManager cdmScript;
	CardTextScript textFetchScript;
	public GameObject cardObj;
	RuleEngine ruleScript;
	Vector3 cardOriginalPosition;
	bool addedCard = false;

	//bool dragComplete = false;
	int moves = 0;
	int score = 0;
	public Text moveText;
	void Start ()
	{
		cardManager = GameObject.Find ("CardManagerObj");
		cdmScript = cardManager.GetComponent<CardManager> ();
		textFetchScript = cardManager.GetComponent<CardTextScript> ();
		ruleScript = cardObj.GetComponent<RuleEngine> ();
		textFetchScript.winPanel.gameObject.SetActive(false) ;
	}


	void Update ()
	{
		Debug.Log ("mvscol" + addedCard);
		//Debug.Log ("mvsph" + addedCard);
		if (addedCard == true) {
			moves+= 10;
		}
		textFetchScript.moveText.text = "Moves: " + moves;

	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		Card selectedCard = gameObject.GetComponent<Card> ();
		cardOriginalPosition = selectedCard.GetPosition ();
		columnSelected = cdmScript.PopIfTopCardOfColumn (selectedCard);
		if (columnSelected == null ) {
			columnSelected = cdmScript.PopIfTopCardOfWasteColumn (selectedCard);
			if (columnSelected == null) 
			{
				return;
			}

		}
		DraggedInstance = gameObject;
		_startPosition = transform.position;
		_zDistanceToCamera = Mathf.Abs (_startPosition.z - Camera.main.transform.position.z);

		_offsetToMouse = _startPosition - Camera.main.ScreenToWorldPoint (
			new Vector3 (Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
		);
	}

	public void OnDrag (PointerEventData eventData)
	{
		if (columnSelected == null) {
			return;
		}
		if(Input.touchCount > 1)
			return;
		Card selectedCard = gameObject.GetComponent<Card> ();
		selectedCard.SetPosition(
			Camera.main.ScreenToWorldPoint (
				new Vector3 (
					Input.mousePosition.x,
					Input.mousePosition.y,
					_zDistanceToCamera - maxZHeight))
			+ _offsetToMouse);
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		if (columnSelected == null) {
			return;
		}
		Card selectedCard = gameObject.GetComponent<Card> ();
		Column targetColumn = cdmScript.FindOverlappingColumn (selectedCard);
		PlaceholderColumn targetPhColum = cdmScript.FindOverlappingPlaceHolderColumn (selectedCard);



		bool flipTopCard = false;
		if (targetColumn != null && ruleScript.isMatching (selectedCard, targetColumn.GetTopCard ())) {
			targetColumn.AddCard (selectedCard);
			//moves++;
			addedCard = true;
			//textFetchScript.moveText.text = "Moves: " + moves;

			flipTopCard = true;
		} else if (targetPhColum != null && ruleScript.isPlaceHolderCardMatching(selectedCard, targetPhColum.GetTopCard ())) {
			targetPhColum.AddCard (selectedCard);
			addedCard = true;
			//moves++;
			//textFetchScript.moveText.text = "Moves: " + moves;

			score += 10;

			if (ruleScript.allStacksAreFull (cdmScript.placeholderColumnArray)) {
				Debug.Log ("Won");
				//won dialogbox
				textFetchScript.winPanel.gameObject.SetActive(true) ;
			}
			flipTopCard = true;
		} else {
			columnSelected.AddCard (selectedCard);
			addedCard = false;
		}
		if (flipTopCard) {
			
			if (columnSelected is Column) {
				FlipTopCard (columnSelected);
			}
		}

		columnSelected = null;
		DraggedInstance = null;
		_offsetToMouse = Vector3.zero;
		transform.position = _startPosition;

	}

	void FlipTopCard(BaseColumn column)
	{
		if (!column.IsEmpty()) {
			column.GetTopCard ().Flip ();
		}
	}
}