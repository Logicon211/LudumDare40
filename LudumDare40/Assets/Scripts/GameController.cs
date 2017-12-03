﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public int numberOfBabiesToWin;

	private int currentNumberOfBabies = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void IncrementNumberOfBabies () {
		++currentNumberOfBabies;
		if (currentNumberOfBabies >= numberOfBabiesToWin) {
			//Trigger win condition
		}
	}

	public void Win () {
		//Something happens when you win
		Debug.Log ("You win");
	}

	public void Lose () {
		//Something happens when you lose
		Debug.Log ("You Lose");
	}
}