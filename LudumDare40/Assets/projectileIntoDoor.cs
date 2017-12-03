using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileIntoDoor : MonoBehaviour {

	public Door door;

	public Light spotLight;
	public Light pointLight;

	public AudioClip adopted;
	public AudioClip doorShut;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {

		if (other.tag == "Baby") {

			//If it's not already closed
			if (door.State != 1) {
				if (door.RotationPending == false) StartCoroutine(door.Move());

				//Score some points. make some noise. close the damn door
				spotLight.enabled = false;
				pointLight.enabled = false;

				Destroy(other.gameObject);

				audioSource.PlayOneShot (adopted);

				audioSource.clip = doorShut;
				audioSource.PlayDelayed (0.6f);
			}

		}
	}
}
