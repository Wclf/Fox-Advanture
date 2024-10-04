using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealth;
    [SerializeField] private Image currentHealth;

    private void Start()
    {
        totalHealth.fillAmount = Health.currentHealth / 10;
    }

    private void Update()
    {
        currentHealth.fillAmount = Health.currentHealth / 10;
    }
}
