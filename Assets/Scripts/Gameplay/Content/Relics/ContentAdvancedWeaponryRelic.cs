using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAdvancedWeaponryRelic : GameRelic
{
    public ContentAdvancedWeaponryRelic()
    {
        m_name = "Advanced Weaponry";
        m_desc = "Allied units with at least <b>Range 2</b> gain +1 attack range.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.Range);
    }
}
