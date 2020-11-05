using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMightOfSugoRelic : GameRelic
{
    public ContentMightOfSugoRelic()
    {
        m_name = "Might of Sugo";
        m_desc = "All allied units get +15/+15.";
        m_rarity = GameRarity.Special;

        LateInit();
    }
}