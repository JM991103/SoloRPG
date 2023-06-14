using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// 적이 이동할 목표의 트랜스폼
    /// </summary>
    Transform waypointTarget;

    /// <summary>
    /// 적의 이동 속도
    /// </summary>
    public float moveSpeed = 3.0f;

    /// <summary>
    /// 시야 범위
    /// </summary>
    public float sightRange = 10.0f;

    public float closeSightRange = 2.5f;

    /// <summary>
    /// 시야각의 절반
    /// </summary>
    public float sightHalfAngle = 50.0f;

    /// <summary>
    /// 추적할 플레이어의 트랜스폼
    /// </summary>
    Transform chaseTarget;

    /// <summary>
    /// 적의 상태를 나타내기 위한 enum
    /// </summary>
    protected enum EnemyState
    {
        Wait = 0,   // 대기 상태
        Chase,      // 추적 상태
        Attack,     // 공격 상태
        Dead        // 사망 상태
    }

    EnemyState state = EnemyState.Chase;
    public float waitTime = 1.0f;
    float waitTimer;

    Animator anim;
    NavMeshAgent agent;
    Rigidbody rigid;

    public float attackPower = 10.0f;
    public float defencePower = 3.0f;
    public float maxHP = 100.0f;    // 최대 HP
    float hp = 100.0f;              // 현재 HP

    float attackSpeed = 1.0f;
    float attackCooltime = 1.0f;

    protected EnemyState State
    {
        get => state;
        set
        {
            if (state != value)
            {
                state = value;

                switch (state)
                {
                    case EnemyState.Wait:
                        anim.SetTrigger("Stop");
                        break;
                    case EnemyState.Chase:
                        anim.SetTrigger("Move");
                        break;
                    case EnemyState.Attack:
                        anim.SetTrigger("Attack");
                        break;
                    case EnemyState.Dead:
                        anim.SetTrigger("Die");
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        agent.speed = moveSpeed;

        State = EnemyState.Wait;
        anim.SetTrigger("Stop");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 들어왔음");            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 나갔음");
        }
    }
}
