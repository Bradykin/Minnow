using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTraditionalMethodsRelic : GameRelic
{
    public ContentTraditionalMethodsRelic()
    {
        m_name = "Traditional Methods";
        m_desc = "Starter spells gain 'Draw a card', and Starter units gain +1 Stamina regen.";
        m_rarity = GameRarity.Event;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Knowledgeable);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}
