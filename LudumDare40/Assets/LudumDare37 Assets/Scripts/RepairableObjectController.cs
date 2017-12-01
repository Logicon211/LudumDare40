using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableObjectController : MonoBehaviour, IRepairable {

	private bool clicked = false;
	public RepairableObjectTaskController repairableObjecttaskController;
	private PlayerLineOfSight playerLineOfSiteController;

	// Use this for initialization
	void Start () {
		repairableObjecttaskController = GameObject.FindObjectOfType<RepairableObjectTaskController> ();
		GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
		playerLineOfSiteController = (PlayerLineOfSight)camera.GetComponent (typeof(PlayerLineOfSight));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Repair() {
		if (!clicked && playerLineOfSiteController.GetEquipedItem() != null && playerLineOfSiteController.GetEquipedItem().CompareTag("Wrench")) {
			Debug.Log ("Clicking on Repairable Object");
			clicked = true;
			repairableObjecttaskController.decrementNumberOfRepairableObjects ();
			transform.Find ("Smoke").GetComponent<ParticleSystem> ().Stop ();
			transform.Find ("SparksSubtle").GetComponent<ParticleSystem> ().Stop();
			AudioSource audioSource = GetComponent<AudioSource> ();
			if (audioSource != null) {
				audioSource.PlayDelayed(0.4f);
			}

		}
	}

	public bool getRepaired() {
		return clicked;
	}
}
