using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxTest : MonoBehaviour {

    public LootBox lootBox;
    public BirdManager bird;

    private bool isOpen = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown("p")) {
            bird.DropLootBox();
        }

	}
}
