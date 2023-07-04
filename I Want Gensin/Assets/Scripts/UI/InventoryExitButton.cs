using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryExitButton : MonoBehaviour
{
    Button exitButton;
    Transform inventory;

    PlayerController playerController;

    private void Awake()
    {
        exitButton = GetComponent<Button>();
        inventory = transform.parent.GetComponent<Transform>();
        exitButton.onClick.AddListener(ExitButton);

        playerController = FindObjectOfType<PlayerController>();
    }

    void ExitButton()
    {
        inventory.gameObject.SetActive(false);
        Time.timeScale = 1;
        //playerController.isCursor = false;
    }

}
