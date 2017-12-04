using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public int numberOfDoorsClosed;

	public int currentNumberOfDoors = 0;
	public BabyOverlord BOverlord;

	public Camera mainCamera;
	public Camera LoseCam;
	public Image gameOverImage;
	public Image gameOverImage2;
	public bool fadeGameOver;
	private Color gameoverColor;
	private Color gameoverColor2;
	private int fadePercent;
	public CarSpawner[] carSpawners;
	public AudioSource backgroundMusic;
	public AudioClip gameOverSong;
	private bool notKilled;
	private bool lost;
	public GameObject retryText;

	// Use this for initialization
	void Start () {
		notKilled = true;
		lost = true;
		LoseCam.enabled = false;
		gameoverColor = gameOverImage.color;
		gameoverColor2 = gameOverImage2.color;
		Text text = GameObject.FindGameObjectWithTag ("RemainingDoors").GetComponent<Text> ();
		text.text = "Remaining Doors: " + (currentNumberOfDoors - numberOfDoorsClosed);
		gameoverColor.a = 0;
		gameoverColor2.a = 0;
		gameOverImage.color = gameoverColor;
		gameOverImage2.color = gameoverColor2;
		fadeGameOver = false;
		fadePercent = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (fadeGameOver) {
			//Debug.Log ("fadePercent: " + fadePercent);
			fadePercent++;
			fadePercent = Mathf.Clamp (fadePercent, 0, 10);
			gameoverColor.a = fadePercent;
			gameoverColor2.a = fadePercent;
			gameOverImage.color = gameoverColor;
			gameOverImage2.color = gameoverColor2;
			backgroundMusic.volume =(fadePercent / 10);
			if (fadePercent >= 10 && notKilled) {
				notKilled = false;
				killAll ();
			}
			if (!notKilled) {
				if (Input.anyKeyDown) {
					reloadLevel ();
				}

			}
		}
	}

	public void CloseADoor () {
		numberOfDoorsClosed++;
		Text text = GameObject.FindGameObjectWithTag ("RemainingDoors").GetComponent<Text> ();
		text.text = "Remaining Doors: " + (currentNumberOfDoors - numberOfDoorsClosed);
		if (currentNumberOfDoors - numberOfDoorsClosed  <= 0) {
			Win ();
		}
	}

	public void Win () {
		//Something happens when you win
		//LoadingScreenManager.LoadScene(3);
		SceneManager.LoadScene(3);
		Debug.Log ("You win");
	}

	public void Lose () {
		if (lost) {
			lost = false;
			BOverlord.ActivateArmageddon ();
			GameObject[] gos3 = GameObject.FindGameObjectsWithTag ("Door");
			foreach (GameObject go in gos3) {
				Door DoorScript = go.GetComponent<Door> ();
				StartCoroutine (DoorScript.Move ());
			}

		//Something happens when you lose

		//Wait a bit before instantly losing?


		mainCamera.enabled = false;
		LoseCam.enabled = true;

		StartCoroutine ("loseScreenDelay");
		//SceneManager.LoadScene(4);
		//LoadingScreenManager.LoadScene(4);
		Debug.Log ("You Lose");
		}

	}

	public void killAll(){
		backgroundMusic.clip = gameOverSong;
		backgroundMusic.volume = 1;
		backgroundMusic.Play();
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Baby");
		foreach(GameObject go in gos)
			Destroy(go);

		GameObject[] gos2 = GameObject.FindGameObjectsWithTag("Car");
		foreach(GameObject go in gos2)
			Destroy(go);

		BOverlord.gameObject.SetActive (false);

		foreach (CarSpawner go in carSpawners)
			go.gameObject.SetActive (false);


		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		retryText.SetActive(true);

	}

	public void reloadLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	IEnumerator loseScreenDelay()
	{

		yield return new WaitForSeconds(5f);
		fadeGameOver = true;

	}
}
