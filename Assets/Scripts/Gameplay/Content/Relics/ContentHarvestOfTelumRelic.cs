using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHarvestOfTelumRelic : GameRelic
{
    public ContentHarvestOfTelumRelic()
    {
        m_name = "Harvest of Telum";
        m_desc = "Gain an extra 25 gold at the end of each wave.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Gold);
    }
}
