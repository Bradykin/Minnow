using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAlterOfTordrimRelic : GameRelic
{
    public ContentAlterOfTordrimRelic()
    {
        m_name = "Alter of Tordrim";
        m_desc = "Whenever a unit is summoned, give it somewhere between -3/-3 and +7/+7.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Tordrim);
    }
}
