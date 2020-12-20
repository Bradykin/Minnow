using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesignSchematicsRelic : GameRelic
{
    public ContentDesignSchematicsRelic()
    {
        m_name = "Design Schematics";
        m_desc = "When an allied <b>Creation</b> unit dies, it <b>permanently</b> gains +1/+3 and +1 Max Stamina.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.Creation);
        m_tagHolder.AddTag(GameTagHolder.TagType.Reanimate);
        m_tagHolder.AddTag(GameTagHolder.TagType.BuffSpell);
    }
}
