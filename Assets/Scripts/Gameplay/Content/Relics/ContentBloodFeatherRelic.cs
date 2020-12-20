using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBloodFeatherRelic : GameRelic
{
    public ContentBloodFeatherRelic()
    {
        m_name = "Blood Feather";
        m_desc = "When an allied unit survives a hit with 3 health; it gets +10/+0.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.Tank);
        m_tagHolder.AddTag(GameTagHolder.TagType.BuffSpell);
        m_tagHolder.AddTag(GameTagHolder.TagType.Healing);
    }
}