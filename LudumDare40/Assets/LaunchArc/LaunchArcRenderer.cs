using System.Collections;
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

	void Awake () {
		lr = GetComponent<LineRenderer> ();	
		g = Mathf.Abs (Physics.gravity.y);
	}
	// Use this for initialization
	void Start () {
		RenderArc ();
	}

	void OnValidate() {
		//check that lr is not null and that the game is playing
		if(lr != null && Application.isPlaying) {
			RenderArc ();
		}
	}

	// Populating the line renderer with appropriate info
	void RenderArc() {
		lr.SetVertexCount (resolution + 1);
		lr.SetPositions (CalculateArcArray());
	}

	Vector3[] CalculateArcArray() {
		Vector3[] arcArray = new Vector3[resolution + 1];

		radianAngle = Mathf.Deg2Rad * angle;
		float maxDistance = (velocity * velocity * Mathf.Sin (2 * radianAngle)) / g;

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

			//Enable line renderer
			velocity += 0.08f;
			if (velocity > velocityMax) {
				velocity = velocityMax;
			}
			RenderArc();
		}

		//When you let go, fire projectile at the angle the arc is set at
		if (Input.GetMouseButtonUp (1)) {
			//Do stuff to fire projectile

			velocity = velocityStart;
			//Disable line renderer
			RenderArc();
		}
	}
}
