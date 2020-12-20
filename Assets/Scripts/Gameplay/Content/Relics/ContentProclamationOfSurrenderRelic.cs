using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentProclamationOfSurrenderRelic : GameRelic
{
    public ContentProclamationOfSurrenderRelic()
    {
        m_name = "Proclamation of Surrender";
        m_desc = "Whenever <b>Spellcraft</b> is triggered, gain an energy.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Spellcraft);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.LowCost);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.HighCost);
    }
}
