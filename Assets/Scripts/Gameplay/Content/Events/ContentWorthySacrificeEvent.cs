using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWorthySacrificeEvent : GameEvent
{
    public ContentWorthySacrificeEvent(GameTile tile)
    {
        m_name = "Worthy Sacrifice";
        m_eventDesc = "Sometimes great feats require sacrifice. Make an offering: the more impressive the offering, the more powerful the returns.";
        m_tile = tile;
        m_rarity = GameRarity.Uncommon;

        m_optionOne = new GameEventRemoveCardForBoonOption();

        LateInit();
    }
}
