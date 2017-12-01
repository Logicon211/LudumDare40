using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableObjectTaskController : MonoBehaviour {

	private TaskController parentController;
	private int numberOfRepairableObjects;
	private bool taskComplete = false;

	private GameObject fixInstructions;
	private GameObject fixInstructionsDone;

	// Use this for initialization
	void Start () {
		fixInstructions = transform.Find ("FixInstructions").gameObject;
		fixInstructionsDone = transform.Find ("FixInstructionsDone").gameObject;

		fixInstructionsDone.SetActive (false);

		numberOfRepairableObjects = GameObject.FindGameObjectsWithTag ("RepairableObject").Length;
		parentController = GameObject.FindObjectOfType<TaskController> ();
		checkTaskComplete ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void decrementNumberOfRepairableObjects () {
		if (numberOfRepairableObjects != 0) {
			numberOfRepairableObjects--;
			Debug.Log ("Decremented, new number of repairable objects: " + numberOfRepairableObjects);
			checkTaskComplete ();
		}
	}

	private void checkTaskComplete () {
		if (numberOfRepairableObjects == 0 && !taskComplete) {
			taskComplete = true;
			Debug.Log ("Repairable Object task complete");
			parentController.TriggerRepairableObjectTaskComplete ();

			fixInstructionsDone.SetActive (true);
			fixInstructions.SetActive (false);
		}
	}
}
