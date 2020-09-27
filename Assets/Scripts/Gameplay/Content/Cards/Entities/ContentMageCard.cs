using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMageCard : GameCardEntityBase
{
    public ContentMageCard()
    {
        m_entity = new ContentMage();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}
