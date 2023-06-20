using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
    Transform fill;

    private void Awake()
    {
        fill = transform.Find("Fill");

        IHealth target = GetComponentInParent<IHealth>();

        target.onHealthChange += Refresh;
    }

    private void Refresh(float ratio)
    {
        fill.localScale = new Vector3(ratio, 1, 1);
    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
