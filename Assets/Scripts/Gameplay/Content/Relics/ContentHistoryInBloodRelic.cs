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

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Healing);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Tank);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.DamageShield);
    }
}
