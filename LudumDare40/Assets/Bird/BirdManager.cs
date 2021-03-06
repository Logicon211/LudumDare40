﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour {

    public Rigidbody lootBox;
	public GameObject fountain;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(fountain.transform.position, Vector3.up, 20 * Time.deltaTime);

    }

    public void DropLootBox() {
        Rigidbody box;
        box = Instantiate(lootBox, transform.position, Quaternion.identity) as Rigidbody;
        //Rigidbody boxBody = box.GetComponent<Rigidbody>();
        
        //boxBody.velocity = transform.TransformDirection(Vector3.forward * 10);
        //Destroy(this);
    }

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Baby") {
			DropLootBox ();

			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}
