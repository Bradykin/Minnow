using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMorlemainsSkullRelic : GameRelic
{
    public ContentMorlemainsSkullRelic()
    {
        m_name = "Morlemains Skull";
        m_desc = "Whenever an enemy unit is killed, gain 1 energy.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.EnergyRegen);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.HighCost, isReceiver: false);
    }
}
