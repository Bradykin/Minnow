using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGroundskeeperCard : GameCardEntityBase
{
    public ContentGroundskeeperCard()
    {
        m_entity = new ContentGroundskeeper();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Forest);
        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}
