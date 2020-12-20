using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSecretOfTheDeepRelic : GameRelic
{
    public ContentSecretOfTheDeepRelic()
    {
        m_name = "Secret of the Deep";
        m_desc = "Allied <b>Humanoid</b> units gain <b>Waterwalking</b>. They die if they end their turn on water.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Humanoid);
    }
}