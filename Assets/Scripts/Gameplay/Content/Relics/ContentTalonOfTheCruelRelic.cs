using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTalonOfTheCruelRelic : GameRelic
{
    public ContentTalonOfTheCruelRelic()
    {
        m_name = "Talon of the Cruel";
        m_desc = "Allied units, spells, and buildings deal double damage to enemies with <b>Flying</b>.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.DamageSpell);
    }
}
