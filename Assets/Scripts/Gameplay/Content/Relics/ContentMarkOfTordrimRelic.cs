using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMarkOfTordrimRelic : GameRelic
{
    public ContentMarkOfTordrimRelic()
    {
        m_name = "Mark of Tordrim";
        m_desc = "When you summon a unit without a keyword, give it a random keyword.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Vanilla);
    }
}
