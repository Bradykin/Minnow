using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPrimeRibRelic : GameRelic
{
    public ContentPrimeRibRelic()
    {
        m_name = "Prime Rib";
        m_desc = "Allied units can heal past their maximum health.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Healing);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Tank);
    }
}
