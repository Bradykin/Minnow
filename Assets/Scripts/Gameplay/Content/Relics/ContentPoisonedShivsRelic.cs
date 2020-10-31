using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPoisonedShivsRelic : GameRelic
{
    public ContentPoisonedShivsRelic()
    {
        m_name = "Poisoned Shivs";
        m_desc = "Shivs drain 2 Stamina from the target.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Shiv);
        m_tags.AddTag(GameTag.TagType.UtilitySpell);
        m_tags.AddTag(GameTag.TagType.Spellcraft);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
    }
}
