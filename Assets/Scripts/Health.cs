using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    private Animator animator;
    public static float currentHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();    
        currentHealth = startingHealth;
    }




    private void Update()
    {

    }
}
