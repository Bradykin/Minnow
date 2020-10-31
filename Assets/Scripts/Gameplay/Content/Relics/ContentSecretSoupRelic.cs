using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSecretSoupRelic : GameRelic
{
    public ContentSecretSoupRelic()
    {
        m_name = "Secret Soup";
        m_desc = "Increase the Stamina regen of <b>all units</b> by 1.\n<i>(Both enemy and allied)</i>";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.UtilitySpell);
    }
}
