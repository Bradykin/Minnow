using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHistoryInBloodRelic : GameRelic
{
    public ContentHistoryInBloodRelic()
    {
        m_name = "History In Blood";
        m_desc = "All units takes double damage. (Both allied and enemy).";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        //TODO: Alex - Tag refactor: Needs a pull tank tag
        m_tags.AddTag(GameTag.TagType.Healing);
        m_tags.AddTag(GameTag.TagType.Spellpower);
        m_tags.AddTag(GameTag.TagType.DamageShield);
        m_tags.AddTag(GameTag.TagType.DamageSpell);
    }
}
