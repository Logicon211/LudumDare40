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
	private Collider collider;
	private int babyZillaLayer;
	private Vector3 rotationAngle;

	// Use this for initialization
	void Start () {
		Init ();
		StartMovingTowardsBabyZilla ();
	}
	
	// Update is called once per frame
	void Update () {
		MoveTowardsBabyZilla ();
		SquirmOnBabyZilla ();
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
		this.gameObject.layer = 11;
		partOfBabyZilla = true;
		rb.isKinematic = true;
		collider.isTrigger = true;
		rb.useGravity = false;
	}

	public void StartMovingTowardsBabyZilla () {
		ejectedFromBabyZilla = false;
		partOfBabyZilla = false;
		speed = defaultSpeed;
	}

	public void EjectFromBabyZilla (Vector3 startPosition) {
		this.gameObject.layer = 10;
		this.gameObject.transform.parent = null;
		this.transform.position = startPosition;
		partOfBabyZilla = false;
		ejectedFromBabyZilla = true;
		speed = 0f;
		rb.isKinematic = false;
		collider.isTrigger = false;
		rb.useGravity = true;
		Vector3 launchDirection = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * new Vector3 (1f, 1f, 1f);
		rb.AddForce (launchDirection * launchForce, ForceMode.Impulse);
	}

	void OnCollisionEnter(Collision col) {
		if ( ejectedFromBabyZilla && col.gameObject.CompareTag ("Ground")) {
			ejectedFromBabyZilla = false;
			//Figure out how to put this on a delay
			StartMovingTowardsBabyZilla ();
		}
	}

	public void SetBabyZillaLayer (int layer) {
		babyZillaLayer = layer;
	}

	public int GetBabyZillaLayer () {
		return babyZillaLayer;
	}

	private void SquirmOnBabyZilla () {
		if (partOfBabyZilla) {
			this.gameObject.transform.Rotate (rotationAngle * Time.deltaTime, Space.Self);
		}
	}

	void Init () {
		Physics.IgnoreLayerCollision (10, 11);
		Physics.IgnoreLayerCollision (11, 12);
		babyZilla = GameObject.FindGameObjectWithTag ("Babyzilla");
		rb = this.gameObject.GetComponent<Rigidbody> ();
		collider = this.gameObject.GetComponent<Collider> ();
		rotationAngle = new Vector3 (Random.Range (-15f, 15f), Random.Range (-15f, 15f), Random.Range (-15f, 15f));
	}
}
