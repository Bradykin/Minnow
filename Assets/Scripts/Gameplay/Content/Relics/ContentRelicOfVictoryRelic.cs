using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRelicOfVictoryRelic : GameRelic
{
    public ContentRelicOfVictoryRelic()
    {
        m_name = "Relic of Victory";
        m_desc = "Whenever an enemy unit with at least 20 power dies, draw 2 cards and gain 2 energy.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.HighCost);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Knowledgeable);
    }
}
