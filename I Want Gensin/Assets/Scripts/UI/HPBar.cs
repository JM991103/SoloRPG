using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    TextMeshProUGUI hpText;
    TextMeshProUGUI levelText;
    Slider hpSlider;

    private void Awake()
    {
        hpSlider = GetComponent<Slider>();
        hpText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        levelText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }
    
}
