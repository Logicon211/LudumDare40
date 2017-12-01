using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchController : MonoBehaviour, IInteractable {

	private bool clicked = false;
	private TaskController parentController;
	private GameObject[] lights;
	private GameObject phoneInteractable;

	public GameObject lightInstructions;
	public GameObject lightInstructionsDone;

	// Use this for initialization
	void Start () {

		lightInstructionsDone.SetActive (false);

		parentController = GameObject.FindObjectOfType<TaskController> ();
		lights = GameObject.FindGameObjectsWithTag ("Light");
		phoneInteractable = GameObject.FindGameObjectWithTag ("phone");
		foreach (GameObject light in lights) {
			light.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Interact () {
		if (!clicked) {
			Debug.Log ("Hit Light Switch");
			this.transform.Rotate (new Vector3 (1, 0, 0), 180);
			clicked = true;
			Debug.Log ("Light switch task complete");
			foreach (GameObject light in lights) {
				light.SetActive (true);



			}
			PhoneTaskController PTC = FindObjectOfType<PhoneTaskController>();
			PTC.StartRinging ();
			parentController.TriggerLightSwitchTaskComplete ();

			lightInstructionsDone.SetActive (true);
			lightInstructions.SetActive (false);
		}
	}
}
