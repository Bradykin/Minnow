using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDragonDenEvent : GameEvent
{
    public GameDragonDenEvent(GameTile tile)
    {
        m_name = "Dragon's Den";
        m_desc = "A strange den lies here... I wonder what could be inside?";
        m_tile = tile;
        m_icon = UIHelper.GetIconEvent(m_name);

        m_APCost = 2;
    }
}
