using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSpelldancerCard : GameUnitCard
{
    public ContentSpelldancerCard()
    {
        m_unit = new ContentSpelldancer();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
        m_tags.AddTag(GameTag.TagType.Forest);
    }
}
