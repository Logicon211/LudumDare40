using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour {
	private Transform thisGameobject;
	private float VertRotateSpeed;
	private float HoriRotateSpeed;
	private float rotateSpeed;
	float vertical;
	float horizontal;
	public GameObject playerObject_renderer;
	private Renderer bodyRenderer;

	// Use this for initialization
	void Start () {
		VertRotateSpeed = -3f;
		HoriRotateSpeed = 4f;
		thisGameobject= GetComponent<Transform>();
		bodyRenderer = playerObject_renderer.GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate() {
		vertical += VertRotateSpeed * Input.GetAxis("Mouse Y");
		horizontal += HoriRotateSpeed * Input.GetAxis("Mouse X");

		// Clamp pitch:
		vertical = Mathf.Clamp(vertical, -60f, 60f);

		if (vertical < -40) {
			float transparencyPercent = (-40 / vertical) -0.2f;

			Color color = bodyRenderer.material.color;
			color.a = transparencyPercent;
			bodyRenderer.material.color = color;
			//Debug.Log ("transparency percent: " + transparencyPercent);
		
		} else {
			Color color = bodyRenderer.material.color;
			color.a = 1;
			bodyRenderer.material.color = color;
			//Debug.Log ("OPAQUE");
		}

		// Wrap yaw:
		while (horizontal < 0f) {
			horizontal += 360f;
		}
		while (horizontal >= 360f) {
			horizontal -= 360f;
		}

		// Set orientation:
		transform.eulerAngles = new Vector3(vertical, horizontal, 0f);


	


	}
}