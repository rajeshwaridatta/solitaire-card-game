﻿using UnityEngine;
using System.Collections;

public class PutPanelBehind : MonoBehaviour {

	public RectTransform panelRect;

	void Start () {
		panelRect = transform as RectTransform;

	}
	
	// Update is called once per frame
	void Update () {
		panelRect.SetAsLastSibling ();
	}


} 