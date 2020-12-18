using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMemoryOfTheDefenderRelic : GameRelic
{
    public ContentMemoryOfTheDefenderRelic()
    {
        m_name = "Memory of the Defender";
        m_desc = "When an allied <b>Creation</b> unit is summoned gain 1 <b>Magic Power</b>.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Reanimate);
        m_tags.AddTag(GameTag.TagType.Creation);
        m_tags.AddTag(GameTag.TagType.MagicPower);
    }
}
