using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineDestroyerScript : MonoBehaviour {

	public AudioSource FirePoof;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {

		if (other.CompareTag ("Player")) {
			other.gameObject.GetComponent<PlayerHealth> ().SetHealth (100);
		}
		
		IDestroyable destroyableObject = (IDestroyable)other.gameObject.GetComponent (typeof(IDestroyable));
		if (destroyableObject != null) {
			destroyableObject.Destroy ();
			FirePoof.Play();
		}
	}
}
