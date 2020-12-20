using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSymbolOfTheAllianceRelic : GameRelic
{
    public ContentSymbolOfTheAllianceRelic()
    {
        m_name = "Symbol of the Alliance";
        m_desc = "When you summon a unit, if you control a <b>Humanoid</b>, <b>Monster</b>, and <b>Creation</b> unit already, give it <b>Damage Reduction</b> 3.  (The unit played does not count)";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.Tank);
        m_tagHolder.AddTag(GameTagHolder.TagType.Monster);
        m_tagHolder.AddTag(GameTagHolder.TagType.Creation);
        m_tagHolder.AddTag(GameTagHolder.TagType.Humanoid);
    }
}
