using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentImpaliumRelic : GameRelic
{
    public ContentImpaliumRelic()
    {
        m_name = "Impalium";
        m_desc = "<b>Spellcraft</b> triggers twice.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.Spellcraft);
        m_tagHolder.AddTag(GameTagHolder.TagType.Shiv);
        m_tagHolder.AddTag(GameTagHolder.TagType.MagicPower);
    }
}
