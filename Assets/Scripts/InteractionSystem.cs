using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    [Header("Detection Parameters")]
    public Transform detectPosition;
    public LayerMask detectLayer;
    private const float detectRadius = 0.2f;
    public GameObject detecedObject;

    [Header("Examine Fields")]
    public GameObject examineWindow;
    public Image examineImage;
    public Text examineText;
    public bool isExamining;


    void Update()
    {
        if(DetectCheck())
        {
            if(GetItem())
            {
                detecedObject.GetComponent<Item>().Interact();
            }
        }
    }

    bool GetItem()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectCheck()
    {
        Collider2D obj = Physics2D.OverlapCircle(detectPosition.position, detectRadius, detectLayer);
        if(obj == null)
        {
            return false;
        }
        else
        {
            detecedObject = obj.gameObject;
            return true;
        }
    }



    public void ExamineItem(Item item)
    {
        if(isExamining)
        {
            examineWindow.SetActive(false);
            isExamining = false;
        }
        else
        {
            examineImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
            examineText.text = item.descriptionText;
            examineWindow.SetActive(true);
            isExamining = true;
        }

    }
}
