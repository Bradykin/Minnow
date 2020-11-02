using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTalonOfTheMeradominRelic : GameRelic
{
    public ContentTalonOfTheMeradominRelic()
    {
        m_name = "Talon of the Meradomin";
        m_desc = "Allied units get +5/+0.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
    }
}