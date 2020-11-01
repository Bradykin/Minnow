using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBeadsOfProphecyRelic : GameRelic
{
    public ContentBeadsOfProphecyRelic()
    {
        m_name = "Beads of Prophecy";
        m_desc = "When an enemy unit dies, all allied units in range 2 gain 1 stamina.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.StaminaRegen);
    }
}
