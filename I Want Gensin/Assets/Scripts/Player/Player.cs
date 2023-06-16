using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IBattle, IHealth
{
    Animator anim;

    public float attackPower = 10.0f;   // 공격력
    public float defencePower = 3.0f;   // 방어력
    public float maxHP = 100.0f;        // 최대 HP
    float hp = 100.0f;                  // 현재 HP

    bool isAlive = true;    // 살아있는지 죽었는지 확인용

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
    /// 공격용 함수
    /// </summary>
    /// <param name="target">공격할 대상</param>
    public void Attack(IBattle target)
    {
        target?.Defence(AttackPower);
    }

    /// <summary>
    /// 방여용 함수
    /// </summary>
    /// <param name="damage">현재 입은 데미지</param>
    public void Defence(float damage)
    {
        if (isAlive)
        {
            anim.SetTrigger("Hit");
            HP -= (damage - DefancePower);
        }
    }

    /// <summary>
    /// 죽었을 때 실행될 함수
    /// </summary>
    public void Die()
    {
        isAlive = false;
        anim.SetBool("Die",isAlive);       
    }
}
