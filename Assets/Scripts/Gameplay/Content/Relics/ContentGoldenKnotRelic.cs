using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGoldenKnotRelic : GameRelic
{
    public ContentGoldenKnotRelic()
    {
        m_name = "Golden Knot";
        m_desc = "Spells cost 3 more energy, but no spells will exile.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tags.AddTag(GameTag.TagType.HighCost);
        //TODO: Alex.  When you can have tags be either push or pull, this should have a pull exile tag.
    }
}
