using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WinLossChecker : MonoBehaviour {

	public GameObject player;
	//public GameObject gameEndingBarrel;

	private PlayerHealth playerHealthScript;
	//private GameEndingBarrel gameEndingBarrelScript;
	// Use this for initialization
	void Start () {
		playerHealthScript = player.GetComponent<PlayerHealth>();
		//gameEndingBarrelScript = gameEndingBarrel.GetComponent<GameEndingBarrel>();
	}

	// Update is called once per frame
	void Update () {

		if(player == null || playerHealthScript.getDead()) {
			StartCoroutine("LoseGame");
		}

//		if(gameEndingBarrel == null || gameEndingBarrelScript.health <= 0) {
//			StartCoroutine("WinGame");
//		}
	}

	IEnumerator LoseGame() {			
		yield return new  WaitForSeconds(3);  // or however long you want it to wait
		Application.LoadLevel("GameOverScreen");
	}

	IEnumerator WinGame() {			
		yield return new WaitForSeconds(3);  // or however long you want it to wait
		Application.LoadLevel("VictoryScreen");
	}
}