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
        m_tagHolder.AddTag(GameTagHolder.TagType.Healing);
        m_tagHolder.AddTag(GameTagHolder.TagType.MagicPower);
        m_tagHolder.AddTag(GameTagHolder.TagType.DamageShield);
        m_tagHolder.AddTag(GameTagHolder.TagType.DamageSpell);
    }
}
