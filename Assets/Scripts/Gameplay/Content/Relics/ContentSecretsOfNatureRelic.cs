using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSecretsOfNatureRelic : GameRelic
{
    public ContentSecretsOfNatureRelic()
    {
        m_name = "Secrets of Nature";
        m_desc = "Allied units in forests get +8/+8.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Forest);
    }
}
