using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentConjuredImpCard : GameUnitCardBase
{
    public ContentConjuredImpCard()
    {
        m_unit = new ContentConjuredImp();

        m_cost = 0;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Reanimate);
        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}
