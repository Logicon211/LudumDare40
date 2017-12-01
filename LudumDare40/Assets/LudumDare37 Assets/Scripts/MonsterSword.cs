using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSword : MonoBehaviour {
    Animator ani;   // Animation component

    bool isHitting = false;
    // Use this for initialization
    void Start () {
        ani = GetComponentInParent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ani.GetBool("Attack"))
        {
            isHitting = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            isHitting = false;
        }
    }

    public bool GetIsHitting()
    {
        return isHitting;
    }
}
