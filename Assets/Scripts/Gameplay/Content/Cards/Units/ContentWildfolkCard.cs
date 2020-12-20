using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWildfolkCard : GameUnitCard
{
    public ContentWildfolkCard()
    {
        m_unit = new ContentWildfolk();

        m_cost = 2;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Monster);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilityUnit);
    }
}
