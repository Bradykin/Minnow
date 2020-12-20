using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWandererCard : GameUnitCard
{
    public ContentWandererCard()
    {
        m_unit = new ContentWanderer();

        m_cost = 2;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Shiv);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Midrange);
    }
}
