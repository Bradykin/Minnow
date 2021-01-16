using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHoovesOfProductionRelic : GameRelic
{
    public ContentHoovesOfProductionRelic()
    {
        m_name = "Hooves of Production";
        m_desc = "Increase max energy by 2.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.HighCost, 3);
    }
}
