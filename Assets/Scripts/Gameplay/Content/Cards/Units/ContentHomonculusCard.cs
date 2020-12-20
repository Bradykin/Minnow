using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHomonculusCard : GameUnitCard
{
    public ContentHomonculusCard()
    {
        m_unit = new ContentHomonculus();

        m_cost = 1;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Knowledgeable);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.StaminaRegen, isReceiver: false);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilityUnit);
    }
}
