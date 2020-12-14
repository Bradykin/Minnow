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

        m_tags.AddTag(GameTag.TagType.Midrange);
        m_tags.AddTag(GameTag.TagType.HighCost);
        m_tags.AddTag(GameTag.TagType.LowCost);
    }
}
