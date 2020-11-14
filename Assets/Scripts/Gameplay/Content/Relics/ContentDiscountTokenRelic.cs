using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDiscountTokenRelic : GameRelic
{
    public ContentDiscountTokenRelic()
    {
        m_name = "Discount Token";
        m_desc = "Buildings cannot cost more than 75 gold. (Can be used any number of times)";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Gold);
    }
}
