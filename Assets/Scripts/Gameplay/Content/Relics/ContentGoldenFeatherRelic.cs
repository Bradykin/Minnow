using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGoldenFeatherRelic : GameRelic
{
    public ContentGoldenFeatherRelic()
    {
        m_name = "Golden Feather";
        m_desc = "When an allied unit survives a hit with 1 health; you gain 15 gold.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Healing);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Gold);
    }
}