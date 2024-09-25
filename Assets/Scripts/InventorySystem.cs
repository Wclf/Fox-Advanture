using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [Header("General fields")]
    public List<GameObject> items = new List<GameObject>();
    public bool isOpen;

    [Header("UI items section")]
    public GameObject ui_Window;
    public Image[] items_Image;

    [Header("UI item Description")]
    public GameObject ui_Description_Window;
    public Image description_Image;
    public Text description_Title;
    public Text description_Text;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        isOpen = !isOpen;
        ui_Window.SetActive(isOpen);

        Update_UI();
     }

    public void PickUp(GameObject item)
    {
        items.Add(item);
        Update_UI();
    }

    void Update_UI()
    {
        HideAll();

        for(int i = 0; i < items.Count; i ++)
        {
            items_Image[i].sprite = items[i].GetComponent<SpriteRenderer>().sprite;
            items_Image[i].gameObject.SetActive(true);
             
        }
    }

    void HideAll()
    {
        foreach(var i in items_Image) { i.gameObject.SetActive(false); }
        HideDescription();
    }

    public void ShowDescription(int id)
    {
        description_Image.sprite = items_Image[id].sprite;
        description_Title.text = items[id].name;
        description_Text.text = items[id].GetComponent<Item>().descriptionText;
        description_Image.gameObject.SetActive(true);
        description_Title.gameObject.SetActive(true);
        description_Text.gameObject.SetActive(true);

    }

    public void HideDescription()
    {
        description_Image.gameObject.SetActive(false );
        description_Title.gameObject.SetActive(false);
        description_Text.gameObject.SetActive(false);

    }

    public void Consume(int id)
    {
        if (items[id].GetComponent<Item>().itemType == Item.ItemType.Comsumables)
        {
            Debug.Log($"CONSUMED { items[id].name} ");
            items[id].GetComponent<Item>().consumeEvent.Invoke();
            Destroy(items[id],0.1f);
            items.Remove(items[id]);
            Update_UI();


        }
    }

}
