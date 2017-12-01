using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeController : GenericPickupableObject, IPickupable {

	private NukeTaskController nukeTaskController;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		nukeTaskController = GameObject.FindObjectOfType<NukeTaskController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Destroy () {
		base.Destroy ();
		nukeTaskController.decrementNumberOfNukes ();
	}
}
