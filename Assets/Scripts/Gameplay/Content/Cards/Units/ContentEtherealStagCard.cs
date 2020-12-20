using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEtherealStagCard : GameUnitCard
{
    public ContentEtherealStagCard()
    {
        m_unit = new ContentEtherealStag();

        m_cost = 3;

        m_tagHolder.AddPullTag(GameTagHolder.TagType.Midrange);

        FillBasicData();
    }
}
