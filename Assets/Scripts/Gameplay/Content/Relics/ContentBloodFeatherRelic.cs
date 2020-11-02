using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBloodFeatherRelic : GameRelic
{
    public ContentBloodFeatherRelic()
    {
        m_name = "Blood Feather";
        m_desc = "When an allied unit survives a hit with 1 health; it gets +10/+0.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Tank);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Healing);
    }
}