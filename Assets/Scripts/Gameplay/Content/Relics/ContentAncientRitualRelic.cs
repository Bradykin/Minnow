using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAncientRitualRelic : GameRelic
{
    public ContentAncientRitualRelic()
    {
        m_name = "Ancient Ritual";
        m_desc = "Allied <b>Monster</b> units take 1 less stamina to attack, but you draw 4 less cards each turn (to a minimum of 1).";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Monster);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.Knowledgeable);
    }
}
