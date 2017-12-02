using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyController : MonoBehaviour {

	public float speed;
	public GameObject babyZilla;



	// Use this for initialization
	void Start () {
		babyZilla = GameObject.FindGameObjectWithTag ("Babyzilla");
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards (this.transform.position, babyZilla.transform.position, step);
	}
}
