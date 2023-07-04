using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    Button invenButton;
    Transform inventory;
    PlayerController playerController;

    bool isInvenToggle;

    private void Awake()
    {
        invenButton = GetComponent<Button>();

        inventory = transform.parent.GetChild(6).GetComponent<Transform>();

        invenButton.onClick.AddListener(InvenToggle);

        playerController = FindObjectOfType<PlayerController>();
    }

    void InvenToggle()
    {
        Time.timeScale = 0;        
        inventory.gameObject.SetActive(true);        
    }
}
