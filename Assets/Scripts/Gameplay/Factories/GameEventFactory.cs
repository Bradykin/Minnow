using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventFactory
{
    public static GameEvent GetRandomEvent(GameTile tile)
    {
        return new ContentMillitiaEvent(tile);

        /*
        int r = Random.Range(0, 4);

        switch (r)
        {
            case 0:
                return new ContentDragonDenEvent(tile);
            case 1:
                return new ContentWonderousGenieEvent(tile);
            case 2:
                return new ContentOverturnedCartEvent(tile);
            case 3:
                return new ContentMillitiaEvent(tile);
            default:
                return null;
        }*/
    }
}
