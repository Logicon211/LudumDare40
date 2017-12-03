﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchArcRenderer : MonoBehaviour {

	LineRenderer lr;

	public float velocity;
	public float velocityMax;
	public float velocityStart;

	public float angle;
	public int resolution = 10;

	float g; //force of gravity on the y axis
	float radianAngle;

	public GameObject projectile;

	public GameObject sphereObject;
	private GameObject sphere;

	public Transform m_Cam;
	public Transform player;

	private Vector3 m_CamForward;



	void Awake () {
		lr = GetComponent<LineRenderer> ();	
		g = Mathf.Abs (Physics.gravity.y);
	}
	// Use this for initialization
	void Start () {
		lr.enabled = false;
		// RenderArc ();
		//m_Cam = Camera.main.transform;
	}

	void OnValidate() {
		//check that lr is not null and that the game is playing
		if(lr != null && Application.isPlaying) {
			RenderArc ();
		}
	}

	// Populating the line renderer with appropriate info
	void RenderArc() {
		if (sphere != null) {
			Destroy (sphere);
		}
		lr.SetVertexCount (resolution + 1);
		lr.SetPositions (CalculateArcArray());

		Vector3 spherePosition = new Vector3(lr.GetPosition (lr.positionCount - 1).x + transform.position.x, lr.GetPosition (lr.positionCount - 1).y + transform.position.y, lr.GetPosition (lr.positionCount - 1).z + transform.position.z);


		sphere = (GameObject) Instantiate (sphereObject, spherePosition , Quaternion.identity);
		sphere.transform.parent = gameObject.transform;
		sphere.transform.RotateAround (transform.position, Vector3.up, transform.rotation.eulerAngles.y);
	}

	Vector3[] CalculateArcArray() {
		Vector3[] arcArray = new Vector3[resolution + 1];


		//Quaternion.LookRotation (transform.parent.rotation);
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, m_Cam.eulerAngles.y - 90f, transform.eulerAngles.z);

		//radianAngle = Mathf.Deg2Rad * angle;

		//float testAngle = m_Cam.localEulerAngles.x;
		//testAngle = (testAngle > 180) ? testAngle - 360 : testAngle;
		//Debug.Log (testAngle);

		if (-m_Cam.eulerAngles.x >= -61f) {
			//Debug.Log(m_Cam.eulerAngles.x);
			radianAngle = Mathf.Deg2Rad * (-m_Cam.eulerAngles.x - 270f/* - angle*/);//Mathf.Deg2Rad * angle;
		} else {
			//Debug.Log(-m_Cam.eulerAngles.x);
			radianAngle = Mathf.Deg2Rad * (-m_Cam.eulerAngles.x/* - angle*/);//Mathf.Deg2Rad * angle;
		}

		radianAngle = Mathf.Deg2Rad * -179f;//(-testAngle + 60f);
			
		Debug.Log (radianAngle);

		//https://en.wikipedia.org/wiki/Range_of_a_projectile
		float maxDistance = ((velocity * velocity) / (2 * g)) * (1 + Mathf.Sqrt (1 + ((2 * g * this.transform.position.y) / (velocity * velocity * Mathf.Sin (radianAngle) * Mathf.Sin (radianAngle))))) * Mathf.Sin (2 * radianAngle); //(velocity * velocity * Mathf.Sin (2 * radianAngle)) / g;

		for (int i = 0; i <= resolution; i++) {
			float t = (float)i / (float) resolution;
			arcArray [i] = CalculateArcPoint (t, maxDistance);
		}

		return arcArray;
	}

	//calcluate height and distance of each vertex
	Vector3 CalculateArcPoint(float t, float maxDistance) {
		float x = t * maxDistance;
		float y = x * Mathf.Tan (radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos (radianAngle) * Mathf.Cos (radianAngle)));
		return new Vector3 (x, y);
	}

	// Update is called once per frame
	void Update () {
		//Charge up throw distance (To a maximum?)

		if (Input.GetMouseButton (1)) {
			//m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
			//Enable line renderer
			lr.enabled = true;
			velocity += 0.08f;
			if (velocity > velocityMax) {
				velocity = velocityMax;
			}
			RenderArc();
		}

		//When you let go, fire projectile at the angle the arc is set at
		if (Input.GetMouseButtonUp (1)) {
			m_CamForward = m_Cam.forward; //Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
			//Do stuff to fire projectile
			GameObject launchedObject = Instantiate(projectile, transform.position, Quaternion.identity);
			launchedObject.transform.Rotate (new Vector3(0f, 0f, angle));
			launchedObject.transform.RotateAround (transform.position, Vector3.up, transform.rotation.eulerAngles.y);

			//Quaternion yRotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.y,Vector3.up);
			//Quaternion zRotation = Quaternion.AngleAxis(radianAngle,Vector3.forward);
			//var vect : Vector3 = Vector3(0,1,0);
			//vect = quat * vect;

			Vector3 projectileVelocity = Vector3.forward * velocity;
			//Vector3 projectileVelocity = m_CamForward * velocity;

			Vector3 zRotatedVector = Quaternion.AngleAxis(m_Cam.eulerAngles.x/* - angle*/, Vector3.right) * projectileVelocity;
			//have to add 90 degrees cause I'm manually rotating the LaunchSource - 90 degrees so it points in front of him
			Vector3 yRotatedVector = Quaternion.AngleAxis(transform.rotation.eulerAngles.y + 90f, Vector3.up) * zRotatedVector;

			Debug.Log (zRotatedVector);
			launchedObject.GetComponent<Rigidbody> ().velocity = yRotatedVector;

			velocity = velocityStart;
			//Disable line renderer
			lr.enabled = false;

			if (sphere != null) {
				Destroy (sphere);
			}
		}
	}
}
