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

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Creation);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Monster);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Victorious);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.MaxStamina);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);
    }
}
