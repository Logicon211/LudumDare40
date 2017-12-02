using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LootBox : MonoBehaviour {

    public AnimatorStateInfo anim;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<AnimatorStateInfo>();
	}
	
	// Update is called once per frame
	void Update () {
		if (anim.IsName("NewCrate")) {
            Debug.Log("hey it hit it fucker");
        }
	}

}
