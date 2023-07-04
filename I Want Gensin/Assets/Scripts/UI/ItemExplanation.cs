using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemExplanation : MonoBehaviour
{
    public TextMeshProUGUI selectName;
    public TextMeshProUGUI selectItemExplanation;
    public Image selectItemImage;

    //public TextMeshProUGUI SelectName => selectName;

    //public TextMeshProUGUI SelectItemExplanation => selectItemExplanation;

    //public Image SelectItemImage => selectItemImage;

    private void Awake()
    {
        selectName = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        selectItemExplanation = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        selectItemImage = transform.GetChild(2).GetChild(0).GetComponent<Image>();
    }
}
