using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class BabyZillaDashChecker : MonoBehaviour {

	public BabyZillaController controller;
	public ThirdPersonUserControl TPUC;

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
			if (TPUC.charging) {
				controller.EjectBaby ();
				controller.EjectBaby ();
				controller.EjectBaby ();
				controller.EjectBaby ();
				if (!audioSource.isPlaying) {                     
					audioSource.PlayOneShot (punch);        
				}
			}
		}

	}

//	void OnTriggerEnter(Collider collision) {
//		if (collision.gameObject.tag == "Player") {
//			if (TPUC.charging) {
//				controller.EjectBaby ();
//				controller.EjectBaby ();
//				controller.EjectBaby ();
//				controller.EjectBaby ();
//				if (!audioSource.isPlaying) {                     
//					audioSource.PlayOneShot (punch);        
//				}
//			}
//		}
//	}
}
