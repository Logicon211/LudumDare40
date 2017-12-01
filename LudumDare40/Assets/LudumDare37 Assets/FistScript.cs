using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistScript : MonoBehaviour {

    Animator ani;   // Animation component

    Collider isHitting = null;
    // Use this for initialization
    void Start()
    {
        ani = GetComponentInParent<Animator>();
    }


    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isHitting = other;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isHitting = null;
        }
    }

    public Collider GetIsHitting()
    {
        return isHitting;
    }
}

