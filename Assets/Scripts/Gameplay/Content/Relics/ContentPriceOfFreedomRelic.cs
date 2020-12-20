using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPriceOfFreedomRelic : GameRelic
{
    public ContentPriceOfFreedomRelic()
    {
        m_name = "Price of Freedom";
        m_desc = "Whenever a friendly unit ends it's turn at full Stamina (before regen), it loses all stamina and <b>permanently</b> gets +2/+2.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.StaminaRegen);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);
    }
}
