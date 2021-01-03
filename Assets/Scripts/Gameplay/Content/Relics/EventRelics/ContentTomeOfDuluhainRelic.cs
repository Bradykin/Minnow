using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTomeOfDuluhainRelic : GameRelic
{
    public ContentTomeOfDuluhainRelic()
    {
        m_name = "Tome of Duluhain";
        m_desc = "-1 <b>Magic Power</b>, all spells cost 1 less energy.";
        m_rarity = GameRarity.Special;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.HighCost);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Spellcraft);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.UtilitySpell);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.DamageSpell);
    }
}
