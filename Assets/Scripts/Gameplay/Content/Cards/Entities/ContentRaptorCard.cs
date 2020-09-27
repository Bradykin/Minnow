using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRaptorCard : GameCardEntityBase
{
    public ContentRaptorCard()
    {
        m_entity = new ContentRaptor();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}
