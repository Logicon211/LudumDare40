using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable {
	void Pickup (GameObject parent);
	void Release (GameObject parent);
	void Throw (GameObject parent, Vector3 throwDirection, float throwForce);
}
