using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyController : MonoBehaviour {

	public float speed;

	private GameObject babyZilla;
	private bool partOfBabyZilla;


	// Use this for initialization
	void Start () {
		Init ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!partOfBabyZilla) {
			float step = speed * Time.deltaTime;
			this.transform.position = Vector3.MoveTowards (this.transform.position, babyZilla.transform.position, step);
		}
	}

	public void SetPartOfBabyZilla (bool partOfBabyZilla) {
		this.partOfBabyZilla = partOfBabyZilla;
		Debug.Log (this.gameObject.name + " part of BabyZilla: " + this.partOfBabyZilla);
	}

	void Init () {
		babyZilla = GameObject.FindGameObjectWithTag ("Babyzilla");
		partOfBabyZilla = false;
	}
}
