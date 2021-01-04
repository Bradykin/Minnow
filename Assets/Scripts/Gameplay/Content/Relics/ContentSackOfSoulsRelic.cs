using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSackOfSoulsRelic : GameRelic
{
    public ContentSackOfSoulsRelic()
    {
        m_name = "Sack of Souls";
        m_desc = "When a unit dies, gain 3 gold.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Reanimate);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Gold);
    }
}
