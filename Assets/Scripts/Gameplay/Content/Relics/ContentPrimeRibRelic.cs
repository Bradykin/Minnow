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

        //TODO - Alex : Tag Refactor; needs a pull tank tag
        m_tags.AddTag(GameTag.TagType.Healing);
    }
}
