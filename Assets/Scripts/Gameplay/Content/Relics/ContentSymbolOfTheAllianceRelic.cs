using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSymbolOfTheAllianceRelic : GameRelic
{
    public ContentSymbolOfTheAllianceRelic()
    {
        m_name = "Symbol of the Alliance";
        m_desc = "Whenever you summon a unit, if you control a <b>Humanoid</b>, <b>Monster</b>, and <b>Creation</b> unit already, give it <b>Damage Reduction 2</b>.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Tank);
        m_tags.AddTag(GameTag.TagType.Monster);
        m_tags.AddTag(GameTag.TagType.Creation);
        m_tags.AddTag(GameTag.TagType.Humanoid);
    }
}
