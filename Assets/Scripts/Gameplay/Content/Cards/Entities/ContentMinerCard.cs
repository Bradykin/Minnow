using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMinerCard : GameUnitCardBase
{
    public ContentMinerCard()
    {
        m_unit = new ContentMiner();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Gold);
        m_tags.AddTag(GameTag.TagType.Mountain);
    }
}
