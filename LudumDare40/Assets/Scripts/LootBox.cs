using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class LootBox : MonoBehaviour {

    public ParticleSystem particles;
    public AudioSource audio;
    public Animator anim;

    private bool isOpen = false;
	public float boxCooldown;

	// Use this for initialization
	void Start () {
		boxCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
		boxCooldown -= 1 * Time.deltaTime;
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

			ThirdPersonUserControl control;
			if (collision.gameObject.GetComponent<ThirdPersonUserControl> () != null) {
				control = collision.gameObject.GetComponent<ThirdPersonUserControl> ();
			} else {
				control = collision.transform.parent.gameObject.GetComponent<ThirdPersonUserControl> ();
			}

			Debug.Log ("charging: " + control.charging + "     boxcooldown: " + boxCooldown);
			if (control.charging && boxCooldown <=0) {
				if (!isOpen) {
					ActivateLootBox ();
					isOpen = !isOpen;
					boxCooldown = 1f;
				} else {
					DeactivateLootBox ();
					isOpen = !isOpen;
					boxCooldown = 1f;
				}
			}
		}
	}

}
