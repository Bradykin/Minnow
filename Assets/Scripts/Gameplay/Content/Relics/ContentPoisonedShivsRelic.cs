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

        m_tagHolder.AddTag(GameTagHolder.TagType.Shiv);
        m_tagHolder.AddTag(GameTagHolder.TagType.UtilitySpell);
        m_tagHolder.AddTag(GameTagHolder.TagType.Spellcraft);
        m_tagHolder.AddTag(GameTagHolder.TagType.BuffSpell);
    }
}
