using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Image itemImage;
    TextMeshProUGUI itemCount;

    private void Awake()
    {
        itemImage = transform.GetChild(1).GetComponent<Image>();
        itemCount = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        itemCount.text = "0";
    }
}
