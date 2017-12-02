using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxTest : MonoBehaviour {

    public LootBox lootBox;

    private bool isOpen = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space")) {
            if (!isOpen) {
                Debug.Log("FUCK");
                lootBox.ActivateLootBox();
                isOpen = !isOpen;
            }
            else {
                Debug.Log("FUCK");
                lootBox.DeactivateLootBox();
                isOpen = !isOpen;
            }
        
        }
	}
}
