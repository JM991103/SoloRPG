using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interact", menuName = "Scriptable Object/interact", order = 1)]
public class Interact : ScriptableObject
{    
    public string interactName;

    public int itemID;

    public int itemAddCount;

    public Sprite invenIcon;

    public float resetTime;

}
