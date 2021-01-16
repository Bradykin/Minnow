using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMemoryOfTheDefenderRelic : GameRelic
{
    public ContentMemoryOfTheDefenderRelic()
    {
        m_name = "Memory of the Defender";
        m_desc = "At the start of each turn, draw 3 extra cards."; ;
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Knowledgeable);
    }
}
