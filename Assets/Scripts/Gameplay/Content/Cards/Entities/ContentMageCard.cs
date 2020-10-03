using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMageCard : GameUnitCardBase
{
    public ContentMageCard()
    {
        m_unit = new ContentMage();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}
