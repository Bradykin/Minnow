using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHoovesOfProductionRelic : GameRelic
{
    public ContentHoovesOfProductionRelic()
    {
        m_name = "Hooves of Production";
        m_desc = "+1 actions during intermission phase.";
        m_rarity = GameRarity.Starter;

        LateInit();

        m_tagHolder.AddPullTag(GameTagHolder.TagType.Gold);
    }
}
