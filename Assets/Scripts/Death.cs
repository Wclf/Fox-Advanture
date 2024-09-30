using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject player;
    public Vector3 startPositionSave;
    public Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        startPositionSave = transform.position;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("IsHurt");
            player.gameObject.SetActive(false);
            Invoke("RestartGame", 1f);
        }
    }

    void RestartGame()
    {
        player.transform.position = startPositionSave;
        player.gameObject.SetActive(true);

    }

}


