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

        m_tags.AddTag(GameTag.TagType.Forest);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.Tank);
    }
}
