using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHealthFlaskRelic : GameRelic
{
    public ContentHealthFlaskRelic()
    {
        m_name = "Health Flask";
        m_desc = "When allied units are at half health or less, they gain <b>Regenerate 5</b>.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Healing);
    }
}