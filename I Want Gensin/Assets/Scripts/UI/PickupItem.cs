using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    TextMeshProUGUI pickUpItemName;
    Image pickItemImage;

    public TextMeshProUGUI PickUpItemName => pickUpItemName;
    public Image PickItemImage => pickItemImage;

    private void Awake()
    {
        pickUpItemName = GetComponentInChildren<TextMeshProUGUI>();
        pickItemImage = transform.GetChild(0).GetComponent<Image>();
    }
    
    public void OnActiveChange(bool activeChanger)
    {
        this.gameObject.SetActive(activeChanger);
    }
}
