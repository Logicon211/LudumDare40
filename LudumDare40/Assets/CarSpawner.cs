using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {

	public float spawnTime = 200f;
	public  float currentTime;

	public GameObject[] cars;

	// Use this for initialization
	void Start () {
		currentTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;

		if (currentTime >= spawnTime) {
			currentTime = 0f;

			int carIndex = Random.Range (0, cars.Length);

			GameObject car = Instantiate (cars [carIndex], transform.position, Quaternion.identity);
			car.transform.rotation = transform.rotation;
		}
	}
}
