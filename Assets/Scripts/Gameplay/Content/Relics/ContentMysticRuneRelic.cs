using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMysticRuneRelic : GameRelic
{
    public ContentMysticRuneRelic()
    {
        m_name = "Mystic Rune";
        m_desc = "At the start of each turn, draw two additional cards and randomize all energy costs from 0-2.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tags.AddTag(GameTag.TagType.HighCost);
        m_tags.AddTag(GameTag.TagType.Knowledgeable);
    }
}
