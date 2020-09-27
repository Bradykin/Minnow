using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentConjuredImpCard : GameCardEntityBase
{
    public ContentConjuredImpCard()
    {
        m_entity = new ContentConjuredImp();

        m_cost = 0;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Reanimate);
        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}
