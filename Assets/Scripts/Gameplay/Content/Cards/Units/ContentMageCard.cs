using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMageCard : GameUnitCard
{
    public ContentMageCard()
    {
        m_unit = new ContentMage();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}
