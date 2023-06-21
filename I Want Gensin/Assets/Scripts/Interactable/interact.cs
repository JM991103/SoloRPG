using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interact", menuName = "Scriptable Object/interact", order = 1)]
public class interact : ScriptableObject
{    
    public string interactName;

    public int itemCount;

    public Sprite invenIcon;

}
