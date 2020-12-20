using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMageCard : GameUnitCard
{
    public ContentMageCard()
    {
        m_unit = new ContentMage();

        m_cost = 1;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Knowledgeable);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.BuffSpell);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilityUnit);
    }
}
