using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, new Vector3(transform.position.x, 0.0f, transform.position.z), Quaternion.identity);
    }

    public void TestSpawnMethod()
    {
        print("This is the method test");
    }
}
