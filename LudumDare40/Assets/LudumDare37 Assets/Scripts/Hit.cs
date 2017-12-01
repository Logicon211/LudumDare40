using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour {

	public int lifetime = 2;

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake()
	{
		//Destroy(this, lifetime);
	}
}
