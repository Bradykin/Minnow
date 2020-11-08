using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDominerickRefrainRelic : GameRelic
{
    public ContentDominerickRefrainRelic()
    {
        m_name = "Dominerick Refrain";
        m_desc = "+3 <b>Magic Power</b>";
        m_rarity = GameRarity.Special;

        LateInit();

        m_tags.AddTag(GameTag.TagType.MagicPower);
        m_tags.AddTag(GameTag.TagType.Shiv);
    }
}
