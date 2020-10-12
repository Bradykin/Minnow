using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHomonculusCard : GameUnitCard
{
    public ContentHomonculusCard()
    {
        m_unit = new ContentHomonculus();

        m_cost = 1;
        m_playerUnlockLevel = 3;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.StaminaRegen);
    }
}
