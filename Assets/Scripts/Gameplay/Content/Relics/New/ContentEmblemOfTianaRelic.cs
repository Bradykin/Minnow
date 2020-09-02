using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEmblemOfTianaRelic : GameRelic
{
    public ContentEmblemOfTianaRelic()
    {
        m_name = "Emblem of Tiana";
        m_desc = "Waves end 3 turns early.";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
