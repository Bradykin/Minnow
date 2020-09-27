using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWildfolkCard : GameCardEntityBase
{
    public ContentWildfolkCard()
    {
        m_entity = new ContentWildfolk();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Scaler);
        m_tags.AddTag(GameTag.TagType.Reanimate);
        m_tags.AddTag(GameTag.TagType.APRegen);
    }
}
