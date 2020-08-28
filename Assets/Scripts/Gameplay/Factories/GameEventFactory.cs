using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventFactory
{
    public static GameEvent GetRandomEvent(GameTile tile)
    {
        int r = Random.Range(0, 3);

        switch (r)
        {
            case 0:
                return new ContentDragonDenEvent(tile);
            case 1:
                return new ContentWonderousGenieEvent(tile);
            case 2:
                return new ContentOverturnedCartEvent(tile);
            default:
                return null;
        }
    }
}
