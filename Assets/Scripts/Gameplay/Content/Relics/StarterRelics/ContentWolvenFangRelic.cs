using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWolvenFangRelic : GameRelic
{
    public ContentWolvenFangRelic()
    {
        m_name = "Wolven Fang";
        m_desc = "Give all friendly units +3/+0.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);
    }
}
