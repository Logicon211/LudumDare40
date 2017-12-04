using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyController : MonoBehaviour {

	public float defaultSpeed;
	public float launchForce;
	public bool flying;

	private float speed;
	private GameObject babyZilla;
	private bool partOfBabyZilla;
	private Rigidbody rb;
	private Collider collider;
	private int babyZillaLayer;
	private Vector3 rotationAngle;

	void Awake () {
		Init ();
	}

	// Use this for initialization
	void Start () {
		Init ();
		//Debug.Log (flying);
		if (!flying) {
			StartMovingTowardsBabyZilla ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		MoveTowardsBabyZilla ();
		SquirmOnBabyZilla ();
	}

	void MoveTowardsBabyZilla ()
	{
		if (!partOfBabyZilla && !flying) {
			float step = speed * Time.deltaTime;
			this.transform.position = Vector3.MoveTowards (this.transform.position, babyZilla.transform.position, step);
		}
	}

	public void SetPartOfBabyZilla (bool partOfBabyZilla) {
		this.partOfBabyZilla = partOfBabyZilla;
	}

	public void CombineWithBabyZilla () {
		this.gameObject.layer = 11;
		partOfBabyZilla = true;
		rb.isKinematic = true;
		collider.isTrigger = true;
		rb.useGravity = false;
	}

	public void StartMovingTowardsBabyZilla () {
		flying = false;
		partOfBabyZilla = false;
		speed = defaultSpeed;
	}

	public void EjectFromBabyZilla (Vector3 startPosition) {
		this.gameObject.layer = 10;
		this.gameObject.transform.parent = null;
		this.transform.position = startPosition;
		partOfBabyZilla = false;
		InitBabyFlying ();
		Vector3 launchDirection = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * new Vector3 (1f, 1f, 1f);
		rb.AddForce (launchDirection * launchForce, ForceMode.Impulse);
	}

	public void ThrowBaby () {
		//Init ();
		StartCoroutine("disableHitBox");
		InitBabyFlying ();
	}

	private void InitBabyFlying ()
	{
		flying = true;
		speed = 0f;
		rb.isKinematic = false;
		collider.isTrigger = false;
		rb.useGravity = true;
	}

	void OnCollisionEnter(Collision col) {
		if (flying && col.gameObject.CompareTag ("Ground")) {
			flying = false;
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

	IEnumerator disableHitBox()
	{
		GetComponent<CapsuleCollider> ().enabled = false;
		yield return new WaitForSeconds(0.1f);
		GetComponent<CapsuleCollider> ().enabled = true;
	}
}
