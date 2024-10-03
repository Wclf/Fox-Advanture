using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Death : MonoBehaviour
{
    public GameObject player;
    public Vector3 startPositionSave;
    public Animator animator;
    public bool isDeath;
    public static bool isAttack;
    private Collider2D[] colliders;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        startPositionSave = transform.position;
        colliders = GetComponents<Collider2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("IsHurt");                        
            isDeath = true;
            DisableColliders();
            isAttack = true;
            audioManager.PlaySFX(audioManager.death);
            Invoke("RestartGame", 1f);
        }
    }

    void DisableColliders()
    {
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }

    void EnableColliders()
    {
        foreach (var collider in colliders)
        {
            collider.enabled = true; // Enable the collider
        }
    }

    void RestartGame()
    {
        player.gameObject.SetActive(false);
        player.transform.position = startPositionSave;
        isDeath = false;
        isAttack = false;
        EnableColliders();
        player.gameObject.SetActive(true);

    }

}


