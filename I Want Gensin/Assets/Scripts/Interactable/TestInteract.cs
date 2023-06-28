using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestInteract : MonoBehaviour
{
    public string itemName;
    public int itemID;
    public int itemAddCount;
    public Sprite invenIcon;
    public float resetTime;
    bool isAcquisition = false;

    MeshRenderer meshRenderer;
    BoxCollider boxCollider;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        for (int i = 0; i < GameManager.Inst.interacts.Length; i++)
        {
            if (transform.gameObject.name == GameManager.Inst.interacts[i].interactName)
            {
                itemName = GameManager.Inst.interacts[i].interactName;
                itemID = GameManager.Inst.interacts[i].itemID;
                itemAddCount = GameManager.Inst.interacts[i].itemAddCount;
                invenIcon = GameManager.Inst.interacts[i].invenIcon;
                resetTime = GameManager.Inst.interacts[i].resetTime;
            }
        }
    }

    //private void Update()
    //{
    //    if (resetTime >= 0)
    //    {
    //        resetTime -= Time.deltaTime;

    //        if (true)
    //        {
    //            isAcquisition = false;
    //        }
    //    }
    //}

    public void Acquisition()
    {
        if (!isAcquisition)
        {
            meshRenderer.enabled = false;
            boxCollider.enabled = false;
            isAcquisition = true;

            StartCoroutine(ResetTime());
            // 인벤토리에 아이템 넣기
        }
    }

    IEnumerator ResetTime()
    {
        yield return new WaitForSeconds(resetTime);
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
        isAcquisition = false;
    }
}
