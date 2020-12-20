using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentConjuredImpCard : GameUnitCard
{
    public ContentConjuredImpCard()
    {
        m_unit = new ContentConjuredImp();

        m_cost = 0;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Reanimate);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Midrange);
    }
}
