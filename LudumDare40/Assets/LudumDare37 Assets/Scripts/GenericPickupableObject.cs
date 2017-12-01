using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPickupableObject : MonoBehaviour, IPickupable, IDestroyable {

	protected Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Pickup (GameObject parent) {
		SetParent (parent);
		rb.useGravity = false;
		rb.isKinematic = true;
	}

	public void Release(GameObject parent) {
		SetParent (parent);
		rb.useGravity = true;
		rb.isKinematic = false;
	}

	private void SetParent(GameObject parent) {
		if (parent != null) {
			this.transform.parent = parent.transform;
		} else {
			this.transform.parent = null;
		}
	}

	public void Throw (GameObject parent, Vector3 throwDirection, float throwForce) {
		Release (parent);
		rb.AddForce (throwDirection * throwForce, ForceMode.Impulse);
	}

	public virtual void Destroy () {
		Destroy (this.gameObject);
	}
}
