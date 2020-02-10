using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner
{
    private Player.Factory _factory;

    public PlayerSpawner(Player.Factory factory)
    {
        _factory = factory;
    }

    public Player SpawnPlayer()
    {
        return _factory.Create();
    }
}
