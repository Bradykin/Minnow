using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrandPactRelic : GameRelic
{
    public ContentGrandPactRelic()
    {
        m_name = "Grand Pact";
        m_desc = "If you have at least 1 Creation unit, 1 Monster unit, and 1 Humanoid unit in play, all allied units get +1 Stamina regen.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Creation);
        m_tags.AddTag(GameTag.TagType.Monster);
        m_tags.AddTag(GameTag.TagType.Humanoid);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.MaxStamina);
    }
}
