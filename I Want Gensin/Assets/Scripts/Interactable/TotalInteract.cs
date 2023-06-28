using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TotalInteract", menuName = "Scriptable Object/totalInteract", order = 2)]
public class TotalInteract : ScriptableObject
{
    public int itemID;

    public Interact[] interacts;
}
