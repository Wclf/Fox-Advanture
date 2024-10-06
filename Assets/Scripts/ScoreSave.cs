using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreSave : MonoBehaviour
{
    public static float score = 0;
    [SerializeField] private TextMeshProUGUI _scoreText;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Cherry"))
        {
            score++;
            _scoreText.text = score.ToString();
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Gem"))
        {
            score += 10f;
            _scoreText.text = score.ToString();
            Destroy(collision.gameObject);
        }

    }
}
