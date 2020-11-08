using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMaskOfAgesRelic : GameRelic
{
    public ContentMaskOfAgesRelic()
    {
        m_name = "Mask of Ages";
        if (GetRelicLevel() == 0)
        {
            m_desc = "At the start of each turn, draw " + (GetRelicLevel() + 1) + " extra card.";
        }
        else
        {
            m_desc = "At the start of each turn, draw " + (GetRelicLevel() + 1) + " extra cards.";
        }
        m_rarity = GameRarity.Starter;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Knowledgeable);
    }
}
