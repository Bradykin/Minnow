using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSackOfSoulsRelic : GameRelic
{
    public ContentSackOfSoulsRelic()
    {
        m_name = "Sack of Souls";
        m_desc = "When a unit dies, gain 2 gold.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.Gold);
        m_tagHolder.AddTag(GameTagHolder.TagType.Reanimate);
    }
}
