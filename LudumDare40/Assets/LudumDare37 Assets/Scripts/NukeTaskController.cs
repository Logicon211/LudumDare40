using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeTaskController : MonoBehaviour {

	private TaskController parentController;
	private int numberOfNukes;
	private bool taskComplete = false;

	private GameObject nukeInstructions;
	private GameObject nukeInstructionsDone;

	public AudioSource nukeTaskAudio;

	// Use this for initialization
	void Start () {
		nukeInstructions = transform.Find ("NukeEngineInstructions").gameObject;
		nukeInstructionsDone = transform.Find ("NukeEngineInstructionsDone").gameObject;

		nukeInstructionsDone.SetActive (false);

		numberOfNukes = GameObject.FindGameObjectsWithTag ("Nuke").Length;
		parentController = GameObject.FindObjectOfType<TaskController> ();
		checkTaskComplete ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void decrementNumberOfNukes () {

		float volumeIn = 0.70f - (float)numberOfNukes * 0.10f;
		nukeTaskAudio.volume = volumeIn;
		Debug.Log ("Nukes decemented, current volume: " + volumeIn);
		nukeTaskAudio.Play ();

		if (numberOfNukes != 0) {
			numberOfNukes--;


			Debug.Log ("Decremented, new number of nukes: " + numberOfNukes);
			checkTaskComplete ();
		}
	}

	private void checkTaskComplete () {
		if (numberOfNukes == 0 && !taskComplete) {
			taskComplete = true;
			Debug.Log ("Nuke task complete");
			parentController.TriggerNukeTaskComplete ();

			nukeInstructionsDone.SetActive (true);
			nukeInstructions.SetActive (false);
		}
	}
}
