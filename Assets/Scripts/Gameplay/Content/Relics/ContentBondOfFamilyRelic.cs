using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBondOfFamilyRelic : GameRelic
{
    public ContentBondOfFamilyRelic()
    {
        m_name = "Bond of Family";
        m_desc = "Allied <b>Humanoid</b> units gets +4/+0 for each other allied humanoid within range 3, and -4/-0 for each other allied <b>Creation</b> within range 3.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.BuffSpell);
        m_tagHolder.AddTag(GameTagHolder.TagType.Humanoid);
    }
}
