using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLastHopeRelic : GameRelic
{
    public ContentLastHopeRelic()
    {
        m_name = "Last Hope";
        m_desc = "Whenever <b>Spellcraft</b> is triggered, draw a card.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Spellcraft);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Knowledgeable);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.LowCost);
    }
}
