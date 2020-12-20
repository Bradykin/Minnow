using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStormChannelerCard : GameUnitCard
{
    public ContentStormChannelerCard()
    {
        m_unit = new ContentStormChanneler();

        m_cost = 2;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Spellcraft);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.LowCost);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilityUnit);
    }
}
