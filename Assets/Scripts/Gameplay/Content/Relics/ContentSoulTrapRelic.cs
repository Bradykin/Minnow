using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSoulTrapRelic : GameRelic
{
    public ContentSoulTrapRelic()
    {
        m_name = "Soul Trap";
        m_desc = "When an allied unit dies, draw 3 cards.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Knowledgeable);
        m_tags.AddTag(GameTag.TagType.Reanimate);
    }
}
