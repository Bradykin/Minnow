using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMinerCard : GameUnitCard
{
    public ContentMinerCard()
    {
        m_unit = new ContentMiner();

        m_cost = 1;

        FillBasicData();

        m_tagHolder.AddPullTag(GameTagHolder.TagType.Gold);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilityUnit);
    }
}
