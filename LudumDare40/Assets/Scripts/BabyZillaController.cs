using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyZillaController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnTriggerEnter(Collider other) {
		Debug.Log ("Hey");
		if (other.gameObject.CompareTag ("Baby")) {
			other.gameObject.GetComponent<BabyController> ().SetPartOfBabyZilla (true);
		}
	}
}
