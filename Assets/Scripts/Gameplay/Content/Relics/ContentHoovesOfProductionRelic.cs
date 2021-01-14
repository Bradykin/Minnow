using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHoovesOfProductionRelic : GameRelic
{
    public ContentHoovesOfProductionRelic()
    {
        m_name = "Hooves of Production";
        m_desc = "Does nothing."; //nmartino - Rework this
        m_rarity = GameRarity.Starter;

        LateInit();

        m_tagHolder.AddPullTag(GameTagHolder.TagType.Gold);
    }
}
