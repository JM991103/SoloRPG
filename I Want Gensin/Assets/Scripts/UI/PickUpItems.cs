using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    PickupItem[] pickupItems;

    private void Awake()
    {
        pickupItems = GetComponentsInChildren<PickupItem>();
    }

    private void Start()
    {
        for (int i = 0; i < pickupItems.Length; i++)
        {
            pickupItems[i].OnActiveChange(false);
            GameManager.Inst.Player.OnInteract[i] += OnInteractTest;
            GameManager.Inst.Player.OnScanNPC[i] += OnScanNPC;
        }
    }

    private void OnInteractTest(TestInteract scanObj, int i)
    {
        if (scanObj != null)
        {
            pickupItems[i].OnActiveChange(true);
            pickupItems[i].PickUpItemName.text = scanObj.itemName;
            pickupItems[i].PickItemImage.sprite = scanObj.invenIcon;
        }
        else
        {
            pickupItems[i].OnActiveChange(false);
        }
    }

    private void OnScanNPC(NPCScript scanNPC, int i)
    {
        if (scanNPC != null)
        {
            pickupItems[i].OnActiveChange(true);
            pickupItems[i].PickUpItemName.text = scanNPC.gameObject.name;
            pickupItems[i].PickItemImage.sprite = null;
        }
        else
        {
            pickupItems[i].OnActiveChange(false);
        }
    }

}
