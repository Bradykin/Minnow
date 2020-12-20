using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNaturalProtectionRelic : GameRelic
{
    public ContentNaturalProtectionRelic()
    {
        m_name = "Natural Protection";
        m_desc = "Allied units only cost 1 Stamina to move through difficult terrain.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Forest);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.StaminaRegen);
    }
}
