using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTraditionalMethodsRelic : GameRelic
{
    public ContentTraditionalMethodsRelic()
    {
        m_name = "Traditional Methods";
        m_desc = "Starter spells gain 'Draw a card', and Starter units gain +1 Stamina regen.";
        m_rarity = GameRarity.Special;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Knowledgeable);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.StaminaRegen);
    }
}
