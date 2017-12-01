using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTest : MonoBehaviour {

    Animator ani;   // Animation component
    Transform plTransform;
    Vector3 targetLookat;
    BoxCollider weaponCollider;
    CapsuleCollider targetCollider;
    PlayerHealth playerHealth;
    MonsterSword sword;
    GameObject player;
    SpawnController spawnControl;
    MonsterHealth health;
    AudioSource audio;

    public float speed;
    public float attackRange;
    public int attackDamage;    // Attack damage
    public float viewAngle;
    public AudioClip attack;
    public AudioClip spawnSound;
    public AudioClip randomSound;

    int counter = 0;

    float distance;
    bool walking = false;
    bool attackable = false;    // Attack timer, if false, attack can't go throguh
    float attackTimer = 0.7f;
    bool startTimer = false;
    bool audioPlayed = false;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        ani = GetComponent<Animator>();
        plTransform = player.GetComponent<Transform>();
        targetCollider = player.GetComponent<CapsuleCollider>();
        playerHealth = player.GetComponent<PlayerHealth>();
        sword = GetComponentInChildren<MonsterSword>();
        targetLookat = plTransform.position;
        spawnControl = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnController>();
        health = GetComponent<MonsterHealth>();
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (health.IsDead() && !ani.GetBool("Dead"))
        {
			GetComponent<Rigidbody> ().detectCollisions = false;
			GetComponent<Rigidbody> ().isKinematic = true;
            spawnControl.DecrementCounter();
            ani.SetBool("Dead", true);
        }
        if (ani.GetBool("FullyDead"))
        {
            Destroy(gameObject);
        }
        distance = Vector3.Distance(transform.position, plTransform.position);

        // Checking to see if up arrow is up or down, will determine if the walk animation is to be played
        if (distance > attackRange) { 
            ani.SetBool("Walk", true);
            walking = true;
        }
        else
        {
            ani.SetBool("Walk", false);
            walking = false;
            if (!ani.GetBool("Attack"))
            {
                attackTimer = .8f;
                startTimer = true;
                audioPlayed = false;
             
            }
            ani.SetBool("Attack", true);
        }
        if (ani.GetBool("Dead") == false)
        {
            if (startTimer)
                attackTimer -= Time.deltaTime;
            if (attackTimer <= 0.15f && !audioPlayed)
            {
                audioPlayed = true;
                audio.PlayOneShot(attack, 0.7f);
            }
            CheckWalking();
            CheckAttack();
            UpdateLookAt();
        }

        if (playerHealth.getDead())
        {
            print("STOP THE PLAYERS DEAD FUCK");
        }
	}

    // Need to wait for the Attack parameter to be false so the skeleton doesn't slide and perform its attack animation
    void CheckWalking()
    {
        if (walking == true && ani.GetBool("Attack") == false)
        {
            // Making a new vector3 for the target to walk at that ignores the y axis, this is to prevent the model from walking into space
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(plTransform.position.x, 0.0f, plTransform.position.z), speed * Time.deltaTime);
        }
    }

    // Check to see if the attack lands against the player, if so, we will call SetHealth() from, the PlayerHealth script to change
    // their health based on the monsters attack
    void CheckAttack()
    {
        distance = Vector3.Distance(transform.position, plTransform.position);
        Vector3 v = plTransform.position - transform.position;

        if (distance <= (attackRange) && ani.GetBool("Attacking") == false && !health.IsDead() && attackTimer <= 0.0f && attackTimer >= -0.5f)
        {
            playerHealth.SetHealth(attackDamage);
            ani.SetBool("Attacking", true);
            counter++;
            
        }

    }

    // Updates the LookAt for the skeleton, while setting the y position to be the model's y so it doesnt do sick flips
    void UpdateLookAt()
    {
        targetLookat = plTransform.position;
        targetLookat.y = transform.position.y;
        transform.LookAt(targetLookat);
    }
}
