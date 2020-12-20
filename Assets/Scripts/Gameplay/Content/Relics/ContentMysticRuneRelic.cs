using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMysticRuneRelic : GameRelic
{
    public ContentMysticRuneRelic()
    {
        m_name = "Mystic Rune";
        m_desc = "At the start of each turn, draw two additional cards and randomize all energy costs from 0-3.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.HighCost);
        m_tagHolder.AddTag(GameTagHolder.TagType.Knowledgeable);
    }
}
