using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentArmouredMonkCard : GameUnitCard
{
    public ContentArmouredMonkCard()
    {
        m_unit = new ContentArmouredMonk();

        m_cost = 1;

        FillBasicData();

        m_tagHolder.AddPullTag(GameTagHolder.TagType.Tank);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Healing);
    }
}
