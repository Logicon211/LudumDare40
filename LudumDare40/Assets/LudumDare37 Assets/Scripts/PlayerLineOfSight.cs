using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLineOfSight : MonoBehaviour {

	public float interactionDistance;
	RaycastHit objectInRange;
	bool isObjectInRange;
	GameObject objectInHand;
	public LayerMask colliderMask;
	public Text interactionText;
	private GameObject equipedItem;
	public GameObject equipableWrench;
	public GameObject equipableDumbell;
	private Animator animator;

	public float throwForce = 5;

	// Use this for initialization
	void Start () {
		isObjectInRange = false;
		interactionText.text = "";
		equipableWrench.SetActive (false);
		animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay (this.transform.position, this.transform.forward * interactionDistance, Color.magenta);

		IPickupable pickupableObject = null;
		IInteractable interactableObject = null;
		IEquipable equipableObject = null;
		IRepairable repairableObject = null;

		if (Physics.Raycast (this.transform.position, this.transform.forward, out objectInRange, interactionDistance, colliderMask)) {
//			Debug.Log ("Object in range: " + objectInRange.collider.name);

			pickupableObject = (IPickupable)objectInRange.collider.gameObject.GetComponent (typeof(IPickupable));
			interactableObject = (IInteractable)objectInRange.collider.gameObject.GetComponent (typeof(IInteractable));
			equipableObject = (IEquipable)objectInRange.collider.gameObject.GetComponent (typeof(IEquipable));
			repairableObject = (IRepairable)objectInRange.collider.gameObject.GetComponent (typeof(IRepairable));

			isObjectInRange = true;
			if (Input.GetKeyDown(KeyCode.E)) {
				if (objectInHand == null) {
					if (interactableObject != null) {
						interactableObject.Interact ();
					} 
				}

				if (equipedItem == null && equipableObject != null) {
					if (equipableObject is WrenchController) {
						equipedItem = equipableWrench;
						equipableWrench.SetActive (true);
						objectInRange.collider.gameObject.SetActive(false);
						animator.SetBool("hasItem", true);
					}
				}
			}

			if (Input.GetMouseButtonDown (1)) {
				if (objectInHand != null) {
					if (pickupableObject != null) {
						pickupableObject.Throw (null, this.transform.forward, throwForce);
						objectInHand = null;
					}
				}
			}

			if (Input.GetMouseButtonDown (0)) {
				if (objectInHand == null) {
					if (repairableObject != null && equipedItem != null && equipedItem.CompareTag("Wrench")) {
						Debug.Log ("In repair");
						repairableObject.Repair ();
					}
				}
			}

		} else {
			isObjectInRange = false;
		}

		if (Input.GetKeyDown(KeyCode.E)) {
			if (objectInHand != null) {
				((IPickupable) objectInHand.GetComponent<Collider>().gameObject.GetComponent(typeof(IPickupable))).Release (null);
				objectInHand = null;
				Debug.Log ("Release");
			} else if (isObjectInRange) {
				IPickupable interactingWithObject = (IPickupable) objectInRange.collider.gameObject.GetComponent(typeof(IPickupable));
				if (interactingWithObject != null) {
					interactingWithObject.Pickup (this.gameObject);
					objectInHand = objectInRange.collider.gameObject;
					Debug.Log ("Pickup");
				}
			} 
		}

		updateInteractionText (interactableObject, pickupableObject, equipableObject, repairableObject);
	}

    public bool IsAbleToPunch()
    {
        if (objectInHand == null)
            return true;
        return false;
    }

	private void updateInteractionText(IInteractable interactable, IPickupable pickupable, IEquipable equipable, IRepairable repairable) {
		
		if (interactable != null && objectInHand == null) {
			interactionText.text = "[E] to interact";
		} else if (pickupable != null && objectInHand == null) {
			interactionText.text = "[E] to pickup";
		} else if (equipable != null && equipedItem == null && objectInHand == null) {
			interactionText.text = "[E] to equip";
		} else if (repairable != null && equipedItem != null && equipedItem.CompareTag("Wrench")) {
			if (repairable.getRepaired ()) {
				interactionText.text = "Target is Repaired";
			} else {
				interactionText.text = "[Left Click] to repair";
			}
		} else { 
			interactionText.text = "";
		}
	}

	public GameObject GetEquipedItem() {
		return equipedItem;
	}
}
