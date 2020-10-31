using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesignSchematicsRelic : GameRelic
{
    public ContentDesignSchematicsRelic()
    {
        m_name = "Design Schematics";
        m_desc = "When an allied Creation unit dies, return its card to the discard pile.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Creation);
        m_tags.AddTag(GameTag.TagType.Reanimate);
    }
}
