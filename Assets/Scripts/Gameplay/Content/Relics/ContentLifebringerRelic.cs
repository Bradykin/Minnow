using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLifebringerRelic : GameRelic
{
    public ContentLifebringerRelic()
    {
        m_name = "Lifebringer";
        m_desc = "Whenever an allied unit is healed, it gains 1 stamina.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Healing, 3);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.MaxStamina);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.StaminaRegen);
    }
}
