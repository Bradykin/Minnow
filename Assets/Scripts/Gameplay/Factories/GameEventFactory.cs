using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventFactory
{
    public static GameEvent GetRandomEvent(GameTile tile)
    {
        int r = Random.Range(0, 1);

        switch(r)
        {
            case 0:
                return new GameDragonDenEvent(tile);
            default:
                return null;
        }
    }
}
