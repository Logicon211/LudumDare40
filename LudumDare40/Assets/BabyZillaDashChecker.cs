using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class BabyZillaDashChecker : MonoBehaviour {

	public BabyZillaController controller;

	private AudioSource audioSource;
	public AudioClip punch;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Player") {
			if (collision.gameObject.GetComponent<ThirdPersonUserControl>().charging) {
				controller.EjectBaby ();
				controller.EjectBaby ();
				controller.EjectBaby ();
				controller.EjectBaby ();
				audioSource.PlayOneShot (punch);
			}
		}
	}
}
