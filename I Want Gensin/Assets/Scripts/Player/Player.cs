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
    int level = 1;

    bool isAlive = true;    // ����ִ��� �׾����� Ȯ�ο�

    public int Level => level;
    public float AttackPower => attackPower;

    public float DefancePower => defencePower;

    TestInteract scanObj;
    NPCScript scanNPC;

    public Collider[] colliders;

    public Action<TestInteract, int>[] OnInteract = new Action<TestInteract, int>[100];
    public Action<NPCScript, int>[] OnScanNPC = new Action<NPCScript, int>[100];


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
                    Debug.Log("�׾����ϴ�.");
                }

                hp = Mathf.Clamp(hp, 0.0f, maxHP);

                onHealthChange?.Invoke(hp / maxHP);
            }
        }
    }

    public float MaxHP
    {
        get => maxHP;
        set
        {
            if (maxHP != value)
            {
                maxHP = value;                
            }
        }
    }

    public Action<float> onHealthChange { get; set; }
    public Action onDie { get; set; }


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        colliders = Physics.OverlapSphere(transform.position, 2.5f);

        if (colliders != null)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].CompareTag("Interact"))
                {
                    scanObj = colliders[i].gameObject.GetComponent<TestInteract>();

                    if (scanObj != null)
                    {
                        OnInteract[i]?.Invoke(scanObj, i);
                    }
                }
                else if (colliders[i].CompareTag("NPC"))
                {
                    scanNPC = colliders[i].gameObject.GetComponent<NPCScript>();

                    if (scanObj != null)
                    {
                        OnScanNPC[i]?.Invoke(scanNPC, i);
                    }
                }
                else
                {
                    OnInteract[i]?.Invoke(null, i);
                    OnScanNPC[i]?.Invoke(null, i);
                }
            }
        }

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
