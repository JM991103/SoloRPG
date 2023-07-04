using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;

    public Interact[] interacts;

    Inventory inventory;

    public Player Player => player;

    public Inventory Inventory => inventory;

    protected override void Initialize()
    {
        base.Initialize();

        player = FindObjectOfType<Player>();
        inventory = FindObjectOfType<Inventory>();
    }
}
