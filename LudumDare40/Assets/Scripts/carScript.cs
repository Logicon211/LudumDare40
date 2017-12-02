using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carScript : MonoBehaviour {

	Rigidbody rb;

	public float speed = 10f;
	public float m_GroundCheckDistance = 5f;

	private bool m_IsGrounded = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hitInfo;
		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
		{
			m_IsGrounded = true;
			rb.velocity = Vector3.forward * speed;
		}
	}
}
