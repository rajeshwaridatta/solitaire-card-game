
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;




public class CardManager : MonoBehaviour
{
	public GameObject[] cards;
	public GameObject[] slotCards;
	public GameObject wasteSlotCard;
	public Sprite[] signs;
	public Color[] signColor;
	public GameObject[] deckCards;
	public string[] textArray;
	public string[] signNameArray;

	public Sprite frontCard;
	public Sprite backCard;


	//CollisionDetectionCode collisionScript;
	public Button restartButton;
	private List<Column> columnArray ;
	public List<PlaceholderColumn> placeholderColumnArray;
	public int totalNumOfCols ;
	int totalNumOfPlaceholderCols;
	int columnGapX;
	int placeHolderColGapX;
	float columnBaseX;
	float deckCardXpos = -2.0f;
	float deckCardYPos = 10.0f;


	public int totalNumOfCards;
	int signCounter = 0;
	int slotSignCounter = 0;
	int cardCounter = 0;
	int topCardIndex ;
	public int counter = 0;


	WasteColumn openColum;

	// Use this for initialization
	void Start ()
	{
		totalNumOfCols = 7;
		totalNumOfPlaceholderCols = 4;
		columnGapX = 2;

		placeHolderColGapX = 6;
		columnBaseX = 2;
		totalNumOfCards = cards.Length * signs.Length;
		Init ();
		columnArray = new List<Column> ();
		placeholderColumnArray = new List<PlaceholderColumn> ();

	}

	void TaskOnClick()
	{
		
		Shuffle ();
	}


	void Update ()
	{
		counter++;
		if (counter == 1) {
			Shuffle ();
			PopulateColums ();
			PopulatePlaceholderColumns ();
			PopulateWasteCardColumn ();
		}
		if (clickOnDeckToOpenOneCard () ) 
		{
			PopulateWasteCardColumnWithCards ();
		}


	}
	void Init() 
	{
		deckCards = new GameObject[totalNumOfCards];
		for (int i = 0; i < totalNumOfCards; i++)
		{
			deckCards [i] = Instantiate(cards[cardCounter] , new Vector3((-2),10.0f,0.0f),Quaternion.identity) as GameObject;

			Card card = deckCards [i].GetComponent<Card> ();
			card.SetPosition (new Vector3((deckCardXpos + (i * 0.03f)),deckCardYPos,(0.0f + (i * 0.02f))));

			card.Flip ();
			card.name = textArray [cardCounter] + signNameArray [signCounter];
			card.AssignSign (signs [signCounter], 
				textArray [cardCounter],
				signColor [signCounter]);
			cardCounter++;

			if ((i+1) % cards.Length == 0)
			{
				signCounter++;
				cardCounter = 0;
			}
		}
		topCardIndex = totalNumOfCards - 1;
	}

	void PopulateColums()
	{
		for (int i = 0; i < totalNumOfCols; i++) 
		{
			Column column = new Column (columnBaseX + ((columnGapX + Card.WIDTH) * i) + columnGapX);
			for (int k = 0; k < i + 1; k++) 
			{
				
				Card card = deckCards [topCardIndex--].GetComponent<Card> ();
				column.AddCard (card);
			}

			columnArray.Add(column);
			column.GetTopCard ().Flip ();
		}
	}

	void PopulatePlaceholderColumns()
	{
		
		for (int i = 0; i < totalNumOfPlaceholderCols; i++) 
		{
			PlaceholderColumn column =
				new PlaceholderColumn (columnBaseX + ((placeHolderColGapX + Card.WIDTH) * i) + placeHolderColGapX);
			Card slotCard =
				(Instantiate(slotCards[i], new Vector3(0,0.0f,0.0f),Quaternion.identity) as GameObject)
					.GetComponent<Card> ();
			slotCard.AssignSignName(signs[i]);
			column.AddCard (slotCard);
			placeholderColumnArray.Add(column);
		}
	}

	void PopulateWasteCardColumn()
	{
		openColum = new WasteColumn ();
		Card openCol =
			(Instantiate(wasteSlotCard, new Vector3(0.0f, 0.0f, 0.0f),Quaternion.identity) as GameObject)
				.GetComponent<Card> ();
		openColum.AddCard (openCol);
	}

	void Shuffle () 
	{
		System.Random random = new System.Random();
		for( int i = 0; i < totalNumOfCards; i ++ ) {
			int j = random.Next( i, totalNumOfCards  );
			//Debug.Log ("previous i " + deckCards [i] + "jtj " + deckCards [j]);
			GameObject temporary = deckCards[ i ];
			deckCards[ i ] = deckCards[ j ];
			deckCards[ j ] = temporary;
		}
	}

	public Column PopIfTopCardOfColumn( Card cardDragged)
	{
		foreach (Column column in columnArray) {
			if ( !column.IsEmpty() && (cardDragged.name == column.GetTopCard ().name)) 
			{
				column.RemoveCard ();
				return column;
			}
		}
		return null;
	}

	public WasteColumn PopIfTopCardOfWasteColumn( Card cardDragged)
	{
		if (!openColum.IsEmpty () && (cardDragged.name == openColum.GetTopCard ().name) ) {
			openColum.RemoveCard ();
			return openColum;
		}
		return null;
	}

	public Column FindOverlappingColumn(Card card) {
		foreach (Column column in columnArray) {
			if (column.IsEmpty ()) {
				continue;
			}
			Card topCard = column.GetTopCard ();
			if ((Mathf.Abs (topCard.GetPosition ().x - card.GetPosition ().x) < Card.WIDTH)
			    && (Mathf.Abs (topCard.GetPosition ().y - card.GetPosition ().y) < Card.HEIGHT)) {
				return column;
			}
		}
		return null;
	}

	public PlaceholderColumn FindOverlappingPlaceHolderColumn(Card card) {
		foreach (PlaceholderColumn phColumn in placeholderColumnArray) {
			if (phColumn.IsEmpty ()) {
				continue;
			}
			Card topCard = phColumn.GetTopCard ();
			if ((Mathf.Abs (topCard.GetPosition ().x - card.GetPosition ().x) < Card.WIDTH)
				&& (Mathf.Abs (topCard.GetPosition ().y - card.GetPosition ().y) < Card.HEIGHT)) {
				return phColumn;
			}
		}
		return null;
	}


	public bool clickOnDeckToOpenOneCard()
	{
		if(Input.GetButtonDown ("Fire1"))
		{
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint (
				new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
			if(((mousePosition.x > deckCardXpos -Card.WIDTH/2)  && (mousePosition.x < deckCardXpos +   Card.WIDTH/2)) &&
				((mousePosition.y < deckCardYPos + Card.HEIGHT/2)  && (mousePosition.y > deckCardYPos -   Card.HEIGHT/2)))
			{
				return true;
			}	
		}
		return false;
	}

	void PopulateWasteCardColumnWithCards()
	{
		Card toprCardOfDeck = deckCards [topCardIndex].GetComponent<Card> ();
		if (openColum.AddCard (toprCardOfDeck)) {
			topCardIndex--;
			toprCardOfDeck.Flip ();
		}
	}
}

/*************************************************************
 * **************** UTILITY CLASSES **************************
 * ***********************************************************/

public abstract class BaseColumn
{
	protected const float stackCardZGap = 0.25f;

	protected float  xPos;
	protected float  yPos;
	protected float  zPos;
	private Stack<Card> stackOfEachCol = new Stack<Card>();

	public BaseColumn(float x, float y, float z) {
		this.xPos = x;
		this.yPos = y;
		this.zPos = z;
	}

	virtual public bool AddCard( Card cardToAdd)
	{
		int k = stackOfEachCol.Count;
		cardToAdd.SetPosition(new Vector3 (xPos ,yPos - (k * GetVerticalGap()),zPos -(k * stackCardZGap)));
		this.stackOfEachCol.Push (cardToAdd);
		return true;
	}

	virtual public bool IsEmpty() {
		return this.stackOfEachCol.Count == 0;
	}

	public virtual int getStackLength()
	{
		return this.stackOfEachCol.Count;
	}

	public Card GetTopCard()
	{
		return this.stackOfEachCol.Peek ();
	}

	public float GetXPosOfTheCol()
	{
		return this.xPos;
	}

	protected void Remove()
	{
		this.stackOfEachCol.Pop ();
	}

	protected abstract float GetVerticalGap();
}

public class Column : BaseColumn
{
	private const float VERTICAL_GAP = 1.5f;

	public Column(float x) : base(x, 0.0f, 0.0f) {}

	public void RemoveCard()
	{
		base.Remove();
	}

	protected override float GetVerticalGap() {
		return VERTICAL_GAP;
	}
}

public class PlaceholderColumn : BaseColumn
{
	public PlaceholderColumn(float x) : base(x, 8.0f, 0.0f) {}

	protected override float GetVerticalGap() {
		return 0.0f;
	}

	public override int getStackLength() {
		return base.getStackLength () - 1;
	}
}

public class WasteColumn : BaseColumn
{
	private const float VERTICAL_WASTEGAP = 0.0f;
	public WasteColumn() : base(-3.0f, 2.0f, 0.0f) {}

	protected override float GetVerticalGap() {
		return VERTICAL_WASTEGAP;
	}

	public override int getStackLength() {
		return base.getStackLength () - 1;
	}

	public override bool AddCard (Card cardToAdd)
	{
		//if (getStackLength () == 3) {
			//return false;
		//}
		return base.AddCard (cardToAdd);
	}

	public void RemoveCard()
	{
		base.Remove();
	}
}
