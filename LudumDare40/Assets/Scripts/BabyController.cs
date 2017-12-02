using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyController : MonoBehaviour {

	public float defaultSpeed;
	public float launchForce;

	private float speed;
	private GameObject babyZilla;
	private bool partOfBabyZilla;
	private bool ejectedFromBabyZilla;
	private Rigidbody rb;
	private int babyZillaLayer;


	// Use this for initialization
	void Start () {
		Init ();
		StartMovingTowardsBabyZilla ();
	}
	
	// Update is called once per frame
	void Update () {
		MoveTowardsBabyZilla ();
	}

	void MoveTowardsBabyZilla ()
	{
		if (!partOfBabyZilla && !ejectedFromBabyZilla) {
			float step = defaultSpeed * Time.deltaTime;
			this.transform.position = Vector3.MoveTowards (this.transform.position, babyZilla.transform.position, step);
		}
	}

	public void SetPartOfBabyZilla (bool partOfBabyZilla) {
		this.partOfBabyZilla = partOfBabyZilla;
		Debug.Log (this.gameObject.name + " part of BabyZilla: " + this.partOfBabyZilla);
	}

	public void CombineWithBabyZilla () {
		partOfBabyZilla = true;
		rb.isKinematic = true;
	}

	public void StartMovingTowardsBabyZilla () {
		ejectedFromBabyZilla = false;
		partOfBabyZilla = false;
		speed = defaultSpeed;
	}

	public void EjectFromBabyZilla (Vector3 startPosition) {
		this.gameObject.transform.parent = null;
		this.transform.position = startPosition;
		partOfBabyZilla = false;
		ejectedFromBabyZilla = true;
		speed = 0f;
		rb.isKinematic = false;
		Vector3 launchDirection = new Vector3 (1f, 1f, 0f);
		rb.AddForce (launchDirection * launchForce, ForceMode.Impulse);
	}

	void OnCollisionEnter(Collision col) {
		if ( ejectedFromBabyZilla && col.gameObject.CompareTag ("Ground")) {
			ejectedFromBabyZilla = false;
			Invoke("StartMovingTowardsBabyZilla", 3);
		}
	}

	public void SetBabyZillaLayer (int layer) {
		babyZillaLayer = layer;
	}

	public int GetBabyZillaLayer () {
		return babyZillaLayer;
	}

	void Init () {
		babyZilla = GameObject.FindGameObjectWithTag ("Babyzilla");
		rb = this.gameObject.GetComponent<Rigidbody> ();
	}
}
