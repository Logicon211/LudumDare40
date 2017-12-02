using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyZillaController : MonoBehaviour {

	private int numberOfBabies = 0;
	private int maxBabyCapacity = 10;
	private int[] babiesAtEachLevel;

	private List<Vector3> babyPositions;

	// Use this for initialization
	void Start () {
		Init ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Baby")) {
			AddBabyToBabyZilla (other.gameObject);
		}
	}

	private void Init () {
		babiesAtEachLevel = new int[] { 0, 0, 0, 0 };
		babyPositions = new List<Vector3> ();
		babyPositions.Add (new Vector3 (0f, 0f, 0f));
		babyPositions.Add (new Vector3 (1f, 0f, 0f));
		babyPositions.Add (new Vector3 (-1f, 0f, 0f));
		babyPositions.Add (new Vector3 (0f, 0f, 1f));
		babyPositions.Add (new Vector3 (0f, 0f, -1f));
	}

	private void AddBabyToBabyZilla (GameObject baby) {
		baby.GetComponent<BabyController> ().SetPartOfBabyZilla (true);
		int currentLayer = 0;
		while (numberOfBabies < maxBabyCapacity && currentLayer < (babiesAtEachLevel.Length - 1) && babiesAtEachLevel [currentLayer] > babiesAtEachLevel [currentLayer + 1]) {
			currentLayer++;
		}

		if (numberOfBabies < maxBabyCapacity) {
			MakeBabyChildOfBabyZilla (baby, currentLayer);
		} else {
//			Some lose condition
		}
	}

	private void MakeBabyChildOfBabyZilla (GameObject baby, int layer) {
		Vector3 testPosition = babyPositions [babiesAtEachLevel [layer]];
		testPosition.y = layer;
		Debug.Log (testPosition);
		babiesAtEachLevel [layer]++;
	}
}
