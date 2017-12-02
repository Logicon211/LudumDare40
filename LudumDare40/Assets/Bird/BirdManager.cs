using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour {

    public Rigidbody lootBox;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DropLootBox() {
        Rigidbody box;
        box = Instantiate(lootBox, transform.position, transform.rotation) as Rigidbody;
        //Rigidbody boxBody = box.GetComponent<Rigidbody>();
        
        //boxBody.velocity = transform.TransformDirection(Vector3.forward * 10);
        //Destroy(this);
    }
}
