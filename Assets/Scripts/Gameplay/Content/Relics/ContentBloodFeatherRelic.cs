using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBloodFeatherRelic : GameRelic
{
    public ContentBloodFeatherRelic()
    {
        m_name = "Blood Feather";
        m_desc = "DOES NOTHING.";
        m_rarity = GameRarity.Uncommon;

        LateInit();
    }
}