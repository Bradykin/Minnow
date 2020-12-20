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

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Forest);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Tank);
    }
}
