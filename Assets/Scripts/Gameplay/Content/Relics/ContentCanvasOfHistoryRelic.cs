using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCanvasOfHistoryRelic : GameRelic
{
    public ContentCanvasOfHistoryRelic()
    {
        m_name = "Canvas of History";
        m_desc = "When an enemy unit dies, heal all allied units in range 2 for 15.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Tank);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.Healing);
    }
}
