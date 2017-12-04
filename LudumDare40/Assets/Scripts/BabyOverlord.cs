using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyOverlord : MonoBehaviour {

    public BabySpawner[] babies;

	//Timer to spawn
    public float maxTimer = 2f;
	//Timer reduced by difficulty increase rate
    public float difficultyIncreaseRate = 1f;
	//Handles how many enemies are needed to increase the amount of spawners being used at once.
    public int spawnerUnlockRate = 5;
	//how many enemies spawn for difficulty to go up
    public int timerDecreaseRate = 2;


    private bool armageddon = false;

	//Number of active spawners initially
    public int initialSpawners = 1;
	public float lowestSpawnTime = 5f;

	public int enemiesSpawned = 0;
	public int enemiesSpawnedForSpawners = 0;
	public float currentDifficulty = 0f;
	public float timer;


    // Use this for initialization
    void Start() {
        timer = maxTimer;
    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0f) {
            callDownTheBabies();
            enemiesSpawned++;
            enemiesSpawnedForSpawners++;
            if (enemiesSpawned >= timerDecreaseRate) {

                currentDifficulty += difficultyIncreaseRate;
                if (currentDifficulty >= maxTimer - 2f && !armageddon) {
                    currentDifficulty = maxTimer - 2f;
                }
                enemiesSpawned = 0;
            }
            if (enemiesSpawnedForSpawners >= spawnerUnlockRate && initialSpawners < babies.Length) {
                enemiesSpawnedForSpawners = 0;
                initialSpawners++;
            }
            timer = maxTimer - currentDifficulty;

			if (timer < lowestSpawnTime && !armageddon) {
				timer = lowestSpawnTime;
			}
        }
    }

    void callDownTheBabies() {
        //Debug.Log("Starting the birthing communion");
        int[] spawnList = new int[babies.Length];
        for (int i = 0; i < babies.Length; i++) {
            spawnList[i] = i;
        }
        for (int i = 0; i < initialSpawners; i++) {
            int index = Random.Range(0, spawnList.Length);
            babies[spawnList[index]].FormBaby();
            spawnList = NewList(spawnList, index);
        }
    }
    
    int[] NewList(int[] list, int index) {
        int[] returnList = new int[list.Length - 1];
        for (int i = 0; i < index; i++) {
            returnList[i] = list[i];
        }
        for (int i = index + 1; i < list.Length; i++) {
            returnList[i - 1] = list[i];
        }
        string test = "";
        for (int i = 0; i < returnList.Length; i++) {
            test += " " + returnList[i];
        }
        return returnList;
    }

    public void ActivateArmageddon() {
        armageddon = true;
        //currentDifficulty = maxTimer;
		maxTimer = 0.1f;
		timer = 0f;
    }
}
