using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDominerickRefrainRelic : GameRelic
{
    public ContentDominerickRefrainRelic()
    {
        m_name = "Dominerick Refrain";
        m_desc = "Increase spell power by 5";
        m_rarity = GameRarity.Uncommon;

        LateInit();
    }
}
