using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyZillaController : MonoBehaviour {

	public float babyDensity;

	private int maxBabyCapacity = 91;
	private int[] babiesAtEachLevel;

	private List<GameObject> babyList;
	private List<Vector3> babyPositions;
	private GameObject gameController;

	// Use this for initialization
	void Start () {
		Init ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			EjectBaby ();
		}
	}
		
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Baby")) {
			AddBabyToBabyZilla (other.gameObject);
		}
	}

	private void Init () {
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		babiesAtEachLevel = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
		babyPositions = new List<Vector3> ();
		babyList = new List<GameObject> ();
		babyPositions.Add (new Vector3 (0f, 0f, 0f));
		babyPositions.Add (new Vector3 (babyDensity, 0f, 0f));
		babyPositions.Add (new Vector3 (-babyDensity, 0f, 0f));
		babyPositions.Add (new Vector3 (0f, 0f, babyDensity));
		babyPositions.Add (new Vector3 (0f, 0f, -babyDensity));
		babyPositions.Add (new Vector3 (babyDensity, 0f, babyDensity));
		babyPositions.Add (new Vector3 (babyDensity, 0f, -babyDensity));
		babyPositions.Add (new Vector3 (-babyDensity, 0f, babyDensity));
		babyPositions.Add (new Vector3 (-babyDensity, 0f, -babyDensity));
		babyPositions.Add (new Vector3 (2 * babyDensity, 0f, 0f));
		babyPositions.Add (new Vector3 (2 * -babyDensity, 0f, 0f));
		babyPositions.Add (new Vector3 (0f, 0f, 2 * babyDensity));
		babyPositions.Add (new Vector3 (0f, 0f, 2 * -babyDensity));
	}

	private void AddBabyToBabyZilla (GameObject baby) {
		lock (babyPositions) {
			baby.GetComponent<BabyController> ().SetPartOfBabyZilla (true);
			int currentLayer = 0;
			while (babyList.Count < maxBabyCapacity && currentLayer < (babiesAtEachLevel.Length - 1) && babiesAtEachLevel [currentLayer] > babiesAtEachLevel [currentLayer + 1]) {
				++currentLayer;
			}

			if (babyList.Count < maxBabyCapacity) {
				MakeBabyChildOfBabyZilla (baby, currentLayer);
			} else {
				gameController.GetComponent<GameController> ().Lose ();
			}
		}
	}

	private void MakeBabyChildOfBabyZilla (GameObject baby, int layer) {
		int babiesAtLevel = babiesAtEachLevel [layer];
		Vector3 newBabyPosition = babyPositions [babiesAtLevel];
		newBabyPosition.y = layer * babyDensity;
		Debug.Log (newBabyPosition);
		++babiesAtEachLevel [layer];
		baby.GetComponent<BabyController> ().CombineWithBabyZilla ();
		baby.GetComponent<BabyController> ().SetBabyZillaLayer (layer);
		babyList.Add (baby);
		baby.transform.parent = this.gameObject.transform;
		baby.transform.position = this.gameObject.transform.position + newBabyPosition;
	}

	public void EjectBaby () {
		lock (babyPositions) {
			if (babyList.Count > 0) {
				GameObject babyToEject = babyList [babyList.Count - 1];
				babyList.RemoveAt (babyList.Count - 1);
				--babiesAtEachLevel [babyToEject.GetComponent<BabyController> ().GetBabyZillaLayer ()];
				Vector3 startPosition = new Vector3 (0f, 3f, 0f);
				babyToEject.GetComponent<BabyController> ().EjectFromBabyZilla (this.gameObject.transform.position + startPosition);
			}
		}
	}
}
