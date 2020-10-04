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

        m_tags.AddTag(GameTag.TagType.Forest);
        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}
