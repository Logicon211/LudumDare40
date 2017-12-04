﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour {

    public ParticleSystem particles;
    public AudioSource audio;
    public Animator anim;

    private bool isOpen = false;

    public bool testEnabled;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void ActivateLootBox() {
        ChangeAnimationState(1);
        TurnOnParticles();
        TurnOnSound();
    }

    public void DeactivateLootBox() {
        ChangeAnimationState(2);
        TurnOffParticles();
        TurnOffSound();
    }

    void TurnOnParticles() {
        particles.Play();
    }

    void TurnOffParticles() {
        particles.Stop();
    }

    void TurnOnSound() {
        audio.enabled = true;
    }

    void TurnOffSound() {
        audio.enabled = false;
    }

    void ChangeAnimationState(int state) {
        anim.SetInteger("boxState", state);
    }

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player") {
			if (!isOpen) {
				ActivateLootBox();
				isOpen = !isOpen;
			}
			else {
				DeactivateLootBox();
				isOpen = !isOpen;
			}
		}
	}

}
