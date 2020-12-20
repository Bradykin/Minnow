using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentJugOfTordrimRelic : GameRelic
{
    public ContentJugOfTordrimRelic()
    {
        m_name = "Jug of Tordrim";
        m_desc = "Whenever a unit is summoned, swap it's power and health.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Scaler);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);
    }
}
