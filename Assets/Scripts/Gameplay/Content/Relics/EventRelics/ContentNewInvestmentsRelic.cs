using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNewInvestmentsRelic : GameRelic
{
    public ContentNewInvestmentsRelic()
    {
        m_name = "New Investments";
        m_desc = "Gain an extra 35 gold at the end of each wave.";
        m_rarity = GameRarity.Special;

        LateInit();

        m_tagHolder.AddPullTag(GameTagHolder.TagType.Gold);
    }
}
