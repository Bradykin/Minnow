using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEnergyConstructCard : GameUnitCard
{
    public ContentEnergyConstructCard()
    {
        m_unit = new ContentEnergyConstruct();

        m_cost = 3;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.EnergyRegen);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.LowCost);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Midrange);
    }
}
