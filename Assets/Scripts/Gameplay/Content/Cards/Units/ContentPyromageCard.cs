using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPyromageCard : GameUnitCard
{
    public ContentPyromageCard()
    {
        m_unit = new ContentPyromage();

        m_cost = 1;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.BuffSpell, isReceiver: false);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Knowledgeable);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilityUnit);
    }
}
