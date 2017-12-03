using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyOverlord : MonoBehaviour {

    public BabySpawner[] babies;

    public float maxTimer = 2f;
    public float difficulty = 1f;
    public int spawnerUnlockDifficulty = 5;
    public int enemySpawnCounter = 2;

    private int enemiesSpawned = 0;
    private int enemiesSpawnedForSpawners = 0;
    private float currentDifficulty = 0f;
    private float timer;

    private bool armageddon = false;

    private int initialSpawners = 1;


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
            if (enemiesSpawned >= enemySpawnCounter) {

                currentDifficulty += difficulty;
                if (currentDifficulty >= maxTimer - 2f && !armageddon) {
                    currentDifficulty = maxTimer - 2f;
                }
                enemiesSpawned = 0;
            }
            if (enemiesSpawnedForSpawners >= spawnerUnlockDifficulty && initialSpawners < babies.Length) {
                enemiesSpawnedForSpawners = 0;
                initialSpawners++;
            }
            timer = maxTimer - currentDifficulty;
        }
    }

    void callDownTheBabies() {
        Debug.Log("Starting the birthing communion");
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
        currentDifficulty = maxTimer;
    }
}
