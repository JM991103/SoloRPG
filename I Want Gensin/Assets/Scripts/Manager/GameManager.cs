using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;

    public Interact[] interacts;

    public Player Player => player;

    protected override void Initialize()
    {
        base.Initialize();

        player = FindObjectOfType<Player>();
    }
}
