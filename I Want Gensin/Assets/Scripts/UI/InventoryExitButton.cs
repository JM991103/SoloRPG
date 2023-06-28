using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryExitButton : MonoBehaviour
{
    Button exitButton;
    Transform inventory;

    private void Awake()
    {
        exitButton = GetComponent<Button>();
        inventory = transform.parent.GetComponent<Transform>();
        exitButton.onClick.AddListener(ExitButton);
    }

    void ExitButton()
    {
        inventory.gameObject.SetActive(false);
    }

    public void Onadd()
    {
        Debug.Log("Å×½ºÆ®");
    }
}
