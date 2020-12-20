using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShardOfSorrowRelic : GameRelic
{
    public ContentShardOfSorrowRelic()
    {
        m_name = "Shard of Sorrow";
        m_desc = "Spells in your hand cost 1 less per <b>Shiv</b> in your hand.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.Shiv);
        m_tagHolder.AddTag(GameTagHolder.TagType.HighCost);
        m_tagHolder.AddTag(GameTagHolder.TagType.Spellcraft);
    }
}
