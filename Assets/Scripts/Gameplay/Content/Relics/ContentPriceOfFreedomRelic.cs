using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPriceOfFreedomRelic : GameRelic
{
    public ContentPriceOfFreedomRelic()
    {
        m_name = "Price of Freedom";
        m_desc = "Whenever a friendly unit ends it's turn at full Stamina (before regen), it loses all stamina and gets +2/+2.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.Creation);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
    }
}
