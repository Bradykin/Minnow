using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentVoiceOfTheDefenderRelic : GameRelic
{
    public ContentVoiceOfTheDefenderRelic()
    {
        m_name = "Voice of the Defender";
        m_desc = "When an allied <b>Creation</b> unit dies, gain 1 <b>Magic Power</b> <b>permanently</b>.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Reanimate);
        m_tags.AddTag(GameTag.TagType.Creation);
        m_tags.AddTag(GameTag.TagType.MagicPower);
    }
}
