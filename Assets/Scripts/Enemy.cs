using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f; // Speed of the enemy
    public float moveDistance = 3f; // Distance to move in one direction
    public GameObject playerObject;
    public Vector3 playerStartPosition;
    public float restartDelay = 2f;
    public static bool isEnemyDeath = false;
    

    private Vector3 startingPosition;
    private bool movingRight = true;
    private Animator animator;
    
    void Start()
    {
        playerStartPosition = playerObject.transform.position;
        startingPosition = transform.position; // Store the starting position
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        // Calculate the movement direction
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= startingPosition.x + moveDistance)
            {
                movingRight = false; // Change direction
                Flip();
                
            }
        }
        else
        {
            transform.position -= Vector3.right * speed * Time.deltaTime;
            if (transform.position.x <= startingPosition.x - moveDistance)
            {
                movingRight = true; // Change direction
                Flip();

            }
        }
    }

    void Flip()
    {
        if(movingRight == true && transform.position.x == startingPosition.x + moveDistance)
        {
            Vector3 tranformScale = transform.localScale;
            tranformScale.x *= -1f;
            transform.localScale = tranformScale;
        }
        else
        {
            Vector3 tranformScale = transform.localScale;
            tranformScale.x *= -1f;
            transform.localScale = tranformScale;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Die();
            isEnemyDeath = true;
        }
    }

    void Die()
    {
        
        animator.SetTrigger("EnemyDie");
        
        GetComponent<Collider2D>().enabled = false;

        Destroy(gameObject, 1f);
    }




}
