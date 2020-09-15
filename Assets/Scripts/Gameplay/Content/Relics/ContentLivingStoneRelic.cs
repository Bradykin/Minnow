using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLivingStoneRelic : GameRelic
{
    public ContentLivingStoneRelic()
    {
        m_name = "Living Stone";
        m_desc = "All buildings regenerate 1 health per round.";
        m_rarity = GameRarity.Common;

        LateInit();
    }
}
