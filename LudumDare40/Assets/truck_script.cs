using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class truck_script : MonoBehaviour {

	Rigidbody rb;

	public float speed = 10f;
	public float m_GroundCheckDistance = 0.1f;

	private bool m_IsGrounded = false;
	bool timeToDie = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		StartCoroutine ("DieTime");

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
				
		}
	}



	IEnumerator DieTime()
	{
		print("CHARGING");
		yield return new WaitForSeconds(2f);
		timeToDie = true;
		print("DONE CHARGING");


	}
}
