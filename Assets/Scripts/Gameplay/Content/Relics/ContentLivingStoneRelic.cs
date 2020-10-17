using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLivingStoneRelic : GameRelic
{
    public ContentLivingStoneRelic()
    {
        SetRelicLevel(GetRelicLevel());

        m_name = "Living Stone";
        m_desc = "All buildings gain +" + (m_relicLevel + 1) + " max health per round.";
        m_rarity = GameRarity.Starter;

        LateInit();
    }
}
