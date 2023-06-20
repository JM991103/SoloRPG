using System;
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

    private void Start()
    {
        levelText.text = $"Lv.{GameManager.Inst.Player.Level}"; // 레벨 변경

        hpText.text = $"{(int)GameManager.Inst.Player.HP} / {(int)GameManager.Inst.Player.MaxHP}";
        hpSlider.value = Mathf.Clamp(GameManager.Inst.Player.HP, 0, GameManager.Inst.Player.MaxHP);

        GameManager.Inst.Player.onHealthChange += HealthValue;  // HP 변경
    }

    private void HealthValue(float value)
    {
        hpSlider.value = Mathf.Clamp(value, 0, GameManager.Inst.Player.MaxHP);
        hpText.text = $"{(int)GameManager.Inst.Player.HP} / {(int)GameManager.Inst.Player.MaxHP}";
    }
}
