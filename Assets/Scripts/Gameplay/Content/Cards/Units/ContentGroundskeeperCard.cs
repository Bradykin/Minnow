using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGroundskeeperCard : GameUnitCard
{
    public ContentGroundskeeperCard()
    {
        m_unit = new ContentGroundskeeper();

        m_cost = 1;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Forest);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Midrange);
    }
}
