using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    Button invenButton;
    Transform inventory;

    private void Awake()
    {
        invenButton = GetComponent<Button>();

        inventory = transform.parent.GetChild(6).GetComponent<Transform>();

        invenButton.onClick.AddListener(InventToggle);
    }

    void InventToggle()
    {
        inventory.gameObject.SetActive(true);
    }
}
