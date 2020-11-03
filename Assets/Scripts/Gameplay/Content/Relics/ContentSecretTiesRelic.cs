using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSecretTiesRelic : GameRelic
{
    public ContentSecretTiesRelic()
    {
        m_name = "Secret Ties";
        m_desc = "When an allied <b>Creation</b> unit gains max stamina, all allied <b>Monster</b> units within range 3 gain 'Victorious: Get +3/+3'";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Creation);
        m_tags.AddTag(GameTag.TagType.Monster);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Victorious);
        m_tags.AddTag(GameTag.TagType.MaxStamina);
    }
}
