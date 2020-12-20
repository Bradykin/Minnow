using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenSentinelCard : GameUnitCard
{
    public ContentElvenSentinelCard()
    {
        m_unit = new ContentElvenSentinel();

        m_cost = 2;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.StaminaRegen, isReceiver: false);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.BuffSpell);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Scaler);
    }
}
