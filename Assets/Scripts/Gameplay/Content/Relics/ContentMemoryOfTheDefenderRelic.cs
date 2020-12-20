using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMemoryOfTheDefenderRelic : GameRelic
{
    public ContentMemoryOfTheDefenderRelic()
    {
        m_name = "Memory of the Defender";
        m_desc = "When an allied <b>Creation</b> unit is summoned gain 1 <b>Magic Power</b>.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Reanimate);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Creation);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.MagicPower);
    }
}
