using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabySpawner : MonoBehaviour {

    public GameObject babyFab;

    public float maxTimer = 2f;
    public float difficulty = 1f;
    public int enemySpawnCounter = 2;

    private int enemiesSpawned = 0;
    private float currentDifficulty = 0f;
    private float timer;

    private bool armageddon = false;
    

	// Use this for initialization
	void Start () {
        timer = maxTimer;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void FormBaby() {
        Instantiate(babyFab, transform.position, transform.rotation);
    }

    public void ActivateArmageddon() {
        armageddon = true;
        currentDifficulty = maxTimer;
    }
}
