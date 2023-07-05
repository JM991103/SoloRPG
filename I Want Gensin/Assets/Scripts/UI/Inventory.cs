using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    InventorySlot[] inventorySlots;

    ItemExplanation selectItemExplanation;

    int selectSlotIndex;

    Button TestButton;

    private void Awake()
    {
        inventorySlots = GetComponentsInChildren<InventorySlot>();

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].slotsIndex = i;
        }

        selectItemExplanation = GetComponentInChildren<ItemExplanation>();

        TestButton = transform.GetChild(30).GetComponent<Button>();
        TestButton.onClick.AddListener( () => MinusItem(1));
    }

    private void Start()
    {
        this.gameObject.SetActive(false);        
    }

    public void AddItem(TestInteract item, int count)
    {
        bool isItemFind = false;

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].InteractItem != null && inventorySlots[i].InteractItem.itemID == item.itemID)
            {
                // 같은 종류의 아이템이 있으면 아이템 추가
                inventorySlots[i].ItemCount += count;

                isItemFind = true;

                break;
            }
        }

        // 같은 종류의 아이템이 없으면 새로운 슬롯에 아이템 추가
        if (!isItemFind)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].InteractItem == null)
                {
                    inventorySlots[i].InteractItem = item;
                    inventorySlots[i].ItemCount = count;

                    isItemFind = true;

                    break;
                }
            }
        }

        if (!isItemFind)
        {
            Debug.Log("NOOO");
        }
    }

    public void MinusItem(int minusCount)
    {
        if (inventorySlots[selectSlotIndex].ItemCount - minusCount >= 0)
        {
            inventorySlots[selectSlotIndex].ItemCount -= minusCount;

            if (inventorySlots[selectSlotIndex].ItemCount == 0)
            {
                NullselectItemExplanation();
            }
        }
        else 
        {            
            Debug.Log("NO");
        }
    }

    public void ClickSlot(int index)
    {
        selectSlotIndex = index;
        if (inventorySlots[index].InteractItem != null)
        {
            selectItemExplanation.selectName.text = inventorySlots[index].InteractItem.itemName;
            selectItemExplanation.selectItemImage.sprite = inventorySlots[index].InteractItem.invenIcon;
            selectItemExplanation.selectItemExplanation.text = inventorySlots[index].InteractItem.itemExplanation;
        }
        else
        {
            NullselectItemExplanation();
        }
    }

    void NullselectItemExplanation()
    {
        selectItemExplanation.selectName.text = "Item Name";
        selectItemExplanation.selectItemImage.sprite = null;
        selectItemExplanation.selectItemExplanation.text = "아이템 설명칸";
    }
}
