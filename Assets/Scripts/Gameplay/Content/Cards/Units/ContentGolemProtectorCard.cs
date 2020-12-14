using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGolemProtectorCard : GameUnitCard
{
    public ContentGolemProtectorCard()
    {
        m_unit = new ContentGolemProtector();

        m_cost = 6;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Scaler);
        m_tags.AddTag(GameTag.TagType.HighCost);
    }
}
