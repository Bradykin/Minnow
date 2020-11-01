using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentForbiddenKnowledge : GameRelic
{
    public ContentForbiddenKnowledge()
    {
        m_name = "Forbidden Knowledge";
        m_desc = "Whenever you would trigger <b>Knowledgeable</b>, gain an energy.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Knowledgeable);
        m_tags.AddTag(GameTag.TagType.HighCost);
    }
}
