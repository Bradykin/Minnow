using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentInstructionsRelic : GameRelic
{
    public ContentInstructionsRelic()
    {
        m_name = "Instructions";
        m_desc = "Whenever a <b>Creation</b> unit dies it gets +x/+x, where x is it's max Stamina.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Creation);
        m_tags.AddTag(GameTag.TagType.Reanimate);
        m_tags.AddTag(GameTag.TagType.MaxStamina);
    }
}
