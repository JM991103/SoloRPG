using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IBattle, IHealth
{
    Animator anim;

    public float attackPower = 10.0f;   // ���ݷ�
    public float defencePower = 3.0f;   // ����
    public float maxHP = 100.0f;        // �ִ� HP
    float hp = 100.0f;                  // ���� HP

    bool isAlive = true;    // ����ִ��� �׾����� Ȯ�ο�

    public float AttackPower => attackPower;

    public float DefancePower => defencePower;

    public float HP 
    {
        get => hp;
        set
        {
            if (isAlive && hp != value)
            {
                hp = value;

                if (hp < 0)
                {
                    Die();
                }

                hp = Mathf.Clamp(hp, 0.0f, maxHP);

                onHealthChange?.Invoke(hp / maxHP);
            }
        }
    }

    public float MaxHP => maxHP;

    public Action<float> onHealthChange { get; set; }
    public Action onDie { get; set; }


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// ���ݿ� �Լ�
    /// </summary>
    /// <param name="target">������ ���</param>
    public void Attack(IBattle target)
    {
        target?.Defence(AttackPower);
    }

    /// <summary>
    /// �濩�� �Լ�
    /// </summary>
    /// <param name="damage">���� ���� ������</param>
    public void Defence(float damage)
    {
        if (isAlive)
        {
            anim.SetTrigger("Hit");
            HP -= (damage - DefancePower);
        }
    }

    /// <summary>
    /// �׾��� �� ����� �Լ�
    /// </summary>
    public void Die()
    {
        isAlive = false;
        anim.SetBool("Die",isAlive);       
    }
}
