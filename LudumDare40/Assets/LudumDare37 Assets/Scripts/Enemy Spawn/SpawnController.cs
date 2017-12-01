using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public int maxEnemies;
    public float spawnTime;
    public GameObject skeleton;
    public float spawnTimeMultiplier;

    float elapsedTime = 0.0f;
    int spawnPoint = 0;
    int enemyCount = 0;

    EnemySpawn[] spawnPoints;

	private float time;
	private GameObject player;
	public float difficulty = 1f;
	public float difficultyIncreaseTime = 15f;
	public float difficultyIncreaseRate = 1f;
    
    // Use this for initialization
    void Start()
    {
        spawnPoints = GetComponentsInChildren<EnemySpawn>();
		player = GameObject.FindGameObjectWithTag("Player");
		time = 0f;
    }

    // Update is called once per frame
    void Update () {
        elapsedTime += Time.deltaTime;
		if (elapsedTime >= (spawnTime) && enemyCount < maxEnemies)
        {
            AddEnemy();
            enemyCount++;
            elapsedTime = 0.0f;
        }

		time += Time.deltaTime;
		if (time >= difficultyIncreaseTime) {
			spawnTime -= difficulty;
				
			if (spawnTime < 2f) {
				spawnTime = 2f;
			}

			difficulty += difficultyIncreaseRate;
			time = 0;
		}
	}


    void AddEnemy()
    {
		//find closest spawn
		EnemySpawn[] spawnPointsNotClosest = new EnemySpawn[spawnPoints.Length-1];
		EnemySpawn closestSpawn = null;

		foreach (EnemySpawn spawn in spawnPoints) {
			if(closestSpawn == null || Vector3.Distance(closestSpawn.transform.position, player.transform.position) > Vector3.Distance(spawn.transform.position, player.transform.position)) {
				closestSpawn = spawn;
			}
		}
		
		int index = 0;
		foreach (EnemySpawn spawn in spawnPoints) {
			if(closestSpawn != spawn) {
				spawnPointsNotClosest[index] = spawn;
				index++;
			}
		}
		

		spawnPoint = Random.Range(0, spawnPointsNotClosest.Length);
		spawnPointsNotClosest[spawnPoint].SpawnEnemy(skeleton);
    }

    public void DecrementCounter()
    {
        enemyCount--;
        if (enemyCount < 0)
            enemyCount = 0;
        spawnTime *= spawnTimeMultiplier;

		if (spawnTime < 2) {
			spawnTime = 2;
		}
    }
}
