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

        m_tagHolder.AddTag(GameTagHolder.TagType.MagicPower);
        m_tagHolder.AddTag(GameTagHolder.TagType.Shiv);
    }
}
