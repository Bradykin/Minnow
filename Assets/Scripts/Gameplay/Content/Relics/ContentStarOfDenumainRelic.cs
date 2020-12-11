using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStarOfDenumainRelic : GameRelic
{
    public ContentStarOfDenumainRelic()
    {
        m_name = "Star of Denumain";
        m_desc = "Whenever an allied unit is summoned, give it <b>Damage Shield</b>.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tags.AddTag(GameTag.TagType.DamageShield);
        m_tags.AddTag(GameTag.TagType.Tank);
    }
}
