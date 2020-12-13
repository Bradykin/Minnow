using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRhinoProtectorCard : GameUnitCard
{
    public ContentRhinoProtectorCard()
    {
        m_unit = new ContentRhinoProtector();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Forest);
        m_tags.AddTag(GameTag.TagType.Tank);
    }
}
