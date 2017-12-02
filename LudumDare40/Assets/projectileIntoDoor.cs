﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileIntoDoor : MonoBehaviour {

	public Door door;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {

		if (other.tag == "Projectile") {

			//If it's not already closed
			if (door.State != 1) {
				if (door.RotationPending == false) StartCoroutine(door.Move());
				//Score some points. make some noise. close the damn door

				Destroy(other.gameObject);
			}

		}
	}
}
