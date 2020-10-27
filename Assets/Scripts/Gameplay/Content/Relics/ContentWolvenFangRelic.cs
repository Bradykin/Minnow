using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWolvenFangRelic : GameRelic
{
    public ContentWolvenFangRelic()
    {
        m_name = "Wolven Fang";
        m_desc = "Give all friendly units +" + (2 * (1 + GetRelicLevel())) + "/+0.";
        m_rarity = GameRarity.Starter;

        LateInit();
    }
}
