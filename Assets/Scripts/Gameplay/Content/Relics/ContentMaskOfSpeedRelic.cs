using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMaskOfSpeedRelic : GameRelic
{
    public ContentMaskOfSpeedRelic()
    {
        m_name = "Mask of Speed";
        m_desc = "When your hand is empty, draw a card.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.Knowledgeable);
    }
}
