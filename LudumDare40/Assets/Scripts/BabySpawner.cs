using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabySpawner : MonoBehaviour {

    public GameObject babyFab;

    public float maxTimer = 2f;

    private float timer;

	// Use this for initialization
	void Start () {
        timer = maxTimer;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0f) {
            Instantiate(babyFab, transform.position, transform.rotation);
            timer = maxTimer;
        }
	}
}
