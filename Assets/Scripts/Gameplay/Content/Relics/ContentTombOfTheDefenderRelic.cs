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

        m_tags.AddTag(GameTag.TagType.Shiv);
        m_tags.AddTag(GameTag.TagType.MagicPower);
        m_tags.AddTag(GameTag.TagType.Spellcraft);
        m_tags.AddTag(GameTag.TagType.Creation);
        m_tags.AddTag(GameTag.TagType.Reanimate);
    }
}
