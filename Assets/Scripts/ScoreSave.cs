using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreSave : MonoBehaviour
{
    public static float score = 0;
    public static float CherryCount = 0;
    public static float GemCount = 0;

    private float _scoreText;
    [SerializeField] private TextMeshProUGUI _CherryCount;
    [SerializeField] private TextMeshProUGUI _GemCount;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Cherry"))
        {
            CherryCount++;
            _CherryCount.text = CherryCount.ToString();
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Gem"))
        {
            GemCount++;
            _GemCount.text = GemCount.ToString();
            Destroy(collision.gameObject);
        }

    }
}
