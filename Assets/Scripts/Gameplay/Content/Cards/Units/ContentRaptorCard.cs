using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRaptorCard : GameUnitCard
{
    public ContentRaptorCard()
    {
        m_unit = new ContentRaptor();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}
