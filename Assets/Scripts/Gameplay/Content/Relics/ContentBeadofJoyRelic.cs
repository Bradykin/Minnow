using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBeadofJoyRelic : GameRelic
{
    public ContentBeadofJoyRelic()
    {
        m_name = "Bead of Joy";
        m_desc = "Allied units get <b>'Victorious</b>: +1/+1.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Victorious);
    }
}
