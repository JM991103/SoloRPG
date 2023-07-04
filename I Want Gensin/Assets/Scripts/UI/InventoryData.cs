using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData : Singleton<InventoryData>
{
    // �� ������ ������

    Interact[] interacts;

    protected override void Initialize()
    {
        base.Initialize();

        for (int i = 0; i < interacts.Length; i++)
        {
            interacts[i] = GameManager.Inst.interacts[i];
        }
    }

}
