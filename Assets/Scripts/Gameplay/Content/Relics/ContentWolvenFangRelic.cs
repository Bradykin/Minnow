using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWolvenFangRelic : GameRelic
{
    public ContentWolvenFangRelic()
    {
        m_name = "Wolven Fang";
        m_desc = "Increase power of all friendly entities by 2.";
        m_rarity = GameRarity.Common;

        LateInit();
    }
}
