using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Image itemImage;
    TextMeshProUGUI itemCountText;
    Button ImageButton;

    int itemCount;
    TestInteract interactItem;

    public int slotsIndex;

    public int ItemCount
    {
        get => itemCount;
        set
        {
            if (itemCount != value)
            {
                itemCount = value;
                itemCountText.text = itemCount.ToString();

                if (itemCount <= 0)
                {
                    SlotInitialization();
                }
            }
        }
    }

    public TestInteract InteractItem
    {
        get => interactItem;
        set
        {
            if (interactItem != value)
            {
                interactItem = value;

                if (value != null)
                {
                    itemImage.sprite = value.invenIcon; 
                }
            }
        }
    }


    private void Awake()
    {
        itemImage = transform.GetChild(1).GetComponent<Image>();
        itemCountText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

        ImageButton = GetComponentInChildren<Button>();
        ImageButton.onClick.AddListener(ButtonClick);
    }
    
    private void ButtonClick()
    {
        Debug.Log($"{slotsIndex}번째 클릭");

        GameManager.Inst.Inventory.ClickSlot(slotsIndex);
    }

    private void Start()
    {
        SlotInitialization();
    }

    public void SlotInitialization()
    {
        itemImage.sprite = null;
        itemCountText.text = " ";
        itemCount = 0;
        interactItem = null;
    }
}
