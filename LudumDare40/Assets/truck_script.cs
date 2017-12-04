using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class truck_script : MonoBehaviour {

	Rigidbody rb;

	public float speed = 10f;
	public float m_GroundCheckDistance = 0.1f;

	private bool m_IsGrounded = false;
	bool timeToDie = false;

	public AudioClip crash;
	public AudioClip engine;
	public GameObject explosion;
	public GameObject babby;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		StartCoroutine ("DieTime");

		audioSource = GetComponent<AudioSource> ();

	}

	// Update is called once per frame
	void Update () {
		RaycastHit hitInfo;

		if (Physics.Raycast (transform.position + (Vector3.up * 0.1f), -transform.up, out hitInfo, m_GroundCheckDistance)) {
			m_IsGrounded = true;
			// rb.velocity = Vector3.forward * speed;
			
			rb.velocity = transform.forward * speed; //Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * (Vector3.forward * speed);
		
					
			if (timeToDie) {
				transform.rotation = Quaternion.Slerp (
					transform.rotation,
					Quaternion.LookRotation (-transform.right),
					Time.deltaTime * 1f
				);
			}
				

			if (!audioSource.isPlaying) {
				audioSource.clip = engine;
				audioSource.Play ();
			}
		} else {
			audioSource.Stop ();
		}
		}

	void OnCollisionEnter(Collision collision)	{
		if (!(collision.gameObject.tag == "Ground") && !(collision.gameObject.tag =="Baby")) {
			//audioSource.PlayOneShot (crash);


			GameObject launchedObject = Instantiate (babby, transform.position, Quaternion.identity);
			//launchedObject.GetComponent<BabyController> ().ThrowBaby ();

			GameObject launchedObject2 = Instantiate (babby, transform.position, Quaternion.identity);
			//launchedObject2.GetComponent<BabyController> ().ThrowBaby ();

			GameObject launchedObject3 = Instantiate (babby, transform.position, Quaternion.identity);
			//launchedObject3.GetComponent<BabyController> ().ThrowBaby ();


			Instantiate (explosion, transform.position, Quaternion.identity);

			Destroy (gameObject);
		}
	}

	IEnumerator DieTime()
	{
		
		yield return new WaitForSeconds(3f);
		timeToDie = true;
	}
}