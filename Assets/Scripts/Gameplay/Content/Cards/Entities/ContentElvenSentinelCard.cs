using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenSentinelCard : GameUnitCardBase
{
    public ContentElvenSentinelCard()
    {
        m_unit = new ContentElvenSentinel();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}
