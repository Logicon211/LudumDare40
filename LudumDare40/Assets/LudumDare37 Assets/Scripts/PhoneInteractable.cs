using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneInteractable : MonoBehaviour, IInteractable {
	private bool clicked = false;
	public PhoneTaskController phoneTaskController;
	public GameObject phoneOn;
	public GameObject phoneoff;

	void Start(){
//		myLight = transform.FindChild("PointLight").GetComponent<Light>();
		phoneTaskController = GameObject.FindObjectOfType<PhoneTaskController> ();
		phoneoff.SetActive(false);
	
	}    
	void Update(){
	}

	public void Interact() {

		if (!clicked) {

			if (phoneTaskController.CheckRinging ()) {
				Debug.Log ("Clicking on phone");
				//myLight.color = Color.green;
				//pulseSpeed = 6f;
				clicked = true;

				phoneOn.SetActive (false);
				phoneoff.SetActive (true);
				phoneTaskController.StartCall ();
			}
		}
	}
}
