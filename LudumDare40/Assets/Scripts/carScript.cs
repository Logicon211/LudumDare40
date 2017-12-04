using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class carScript : MonoBehaviour {

	Rigidbody rb;

	public float speed = 10f;
	public float m_GroundCheckDistance = 0.1f;

	public AudioClip honk;
	public AudioClip crash;
	public AudioClip engine;
	public AudioClip punch;

	private AudioSource audioSource;

	private bool m_IsGrounded = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hitInfo;
		
		if (Physics.Raycast (transform.position + (Vector3.up * 0.1f), -transform.up, out hitInfo, m_GroundCheckDistance)) {
			m_IsGrounded = true;
			// rb.velocity = Vector3.forward * speed;
			rb.velocity = transform.forward * speed; //Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * (Vector3.forward * speed);


			if (!audioSource.isPlaying) {
				audioSource.clip = engine;
				audioSource.Play ();
			}
		} else {
			audioSource.Stop ();
		}
	}
		
	void OnCollisionEnter(Collision collision)	{
		if (collision.gameObject.tag == "Car") {
			audioSource.PlayOneShot (crash);
		} else if (collision.gameObject.tag == "Player") {
			ThirdPersonUserControl control;
			if (collision.gameObject.GetComponent<ThirdPersonUserControl> () != null) {
				control = collision.gameObject.GetComponent<ThirdPersonUserControl> ();
			} else {
				control = collision.transform.parent.gameObject.GetComponent<ThirdPersonUserControl> ();
			}

			if (control.charging) {
				audioSource.PlayOneShot (punch);
			} else {
				audioSource.PlayOneShot (crash);
			}
		}
	}
}
