using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    Transform target;
    Vector3 offset = Vector3.zero;

    private void Start()
    {
        target = GameManager.Inst.Player.transform;
        offset = transform.position - target.position;
    }

    float moveSpeed = 0.3f;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime);
    }
}
