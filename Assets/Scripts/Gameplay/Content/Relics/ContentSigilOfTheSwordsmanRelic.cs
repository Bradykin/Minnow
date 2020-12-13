using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSigilOfTheSwordsmanRelic : GameRelic
{
    public ContentSigilOfTheSwordsmanRelic()
    {
        m_name = "Sigil of the Swordsman";
        m_desc = "Allied <b>Humanoid</b> units get +8/+0.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Humanoid);
    }
}
