using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public int numberOfBabiesToWin;

	private int currentNumberOfBabies = 0;
	public BabyOverlord BOverlord;

	public Camera mainCamera;
	public Camera LoseCam;
	public Image gameOverImage;
	public Image gameOverImage2;
	public bool fadeGameOver;
	private Color gameoverColor;
	private int fadePercent;
	public CarSpawner[] carSpawners;

	// Use this for initialization
	void Start () {
		LoseCam.enabled = false;
		gameoverColor = gameOverImage.material.color;
		gameoverColor.a = 0;
		gameOverImage.color = gameoverColor;
		gameOverImage2.color = gameoverColor;
		fadeGameOver = false;
		fadePercent = 0;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (fadeGameOver) {
			fadePercent++;
			Mathf.Clamp (fadePercent, 0, 100);
			gameoverColor.a = fadePercent;
			gameOverImage.color = gameoverColor;
			gameOverImage2.color = gameoverColor;
			if (fadePercent == 100) {
				killAll ();
			}

		}
	}

	public void IncrementNumberOfBabies () {
		++currentNumberOfBabies;
		if (currentNumberOfBabies >= numberOfBabiesToWin) {
			//Trigger win condition
		}
	}

	public void Win () {
		//Something happens when you win
		LoadingScreenManager.LoadScene(3);
		Debug.Log ("You win");
	}

	public void Lose () {
		
		//Something happens when you lose

		//Wait a bit before instantly losing?


		mainCamera.enabled = false;
		LoseCam.enabled = true;
		BOverlord.ActivateArmageddon ();


		StartCoroutine ("loseScreenDelay");
		//SceneManager.LoadScene(4);
		//LoadingScreenManager.LoadScene(4);
		Debug.Log ("You Lose");
	}

	public void killAll(){

		GameObject[] gos = GameObject.FindGameObjectsWithTag("Baby");
		foreach(GameObject go in gos)
			Destroy(go);

		GameObject[] gos2 = GameObject.FindGameObjectsWithTag("Car");
		foreach(GameObject go in gos2)
			Destroy(go);

		BOverlord.gameObject.SetActive (false);

		foreach (CarSpawner go in carSpawners)
			go.gameObject.SetActive (false);

	}

	IEnumerator loseScreenDelay()
	{

		yield return new WaitForSeconds(5f);
		fadeGameOver = true;

	}
}
