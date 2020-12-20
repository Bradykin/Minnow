using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTombOfTheDefenderRelic : GameRelic
{
    public ContentTombOfTheDefenderRelic()
    {
        m_name = "Tomb of the Defender";
        m_desc = "When an allied unit dies, add 3 <b>Shivs</b> to your hand.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Shiv);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Spellcraft);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Reanimate);
    }
}
