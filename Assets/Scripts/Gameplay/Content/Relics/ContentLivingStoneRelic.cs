using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLivingStoneRelic : GameRelic
{
    public ContentLivingStoneRelic()
    {
        m_name = "Living Stone";
        m_desc = "All buildings gain +" + (GetRelicLevel() + 1) + " max health per round.";
        m_rarity = GameRarity.Starter;

        LateInit();
    }
}
