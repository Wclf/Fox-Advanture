using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public Transform posA;
    public Transform posB;
    public float speed = 2f;
    public float chaseRange = 5f;
    public Transform player;
    public Vector3 startPosition;
    public Animator animator;
    

    private Vector3 target;
    private bool movingtoB = true;
    

    public static bool isEnemyDeath = false;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        target = posA.position;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        if(Death.isAttack )
        {
            animator.SetBool("eagleAttack", true);
        }
        else
        {
            animator.SetBool("eagleAttack", false);

        }
    }

    private void FixedUpdate()
    {
        if(FindObjectOfType<Death>().isDeath )
        {
            Invoke("restartPos", 1f);
        }
    }

    void restartPos()
    {
        transform.position = startPosition;

    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = movingtoB ? posB.position : posA.position;
            movingtoB = !movingtoB;
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isEnemyDeath = true;
            animator.SetTrigger("eagleHurt");
            audioManager.PlaySFX(audioManager.killEnemy);

            GetComponent<Collider2D>().enabled = false;

            Destroy(gameObject, 1f);

        }
    }

}
