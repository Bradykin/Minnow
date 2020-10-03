﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOverlordCard : GameUnitCardBase
{
    public ContentOverlordCard()
    {
        m_unit = new ContentOverlord();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Explorer);
        m_tags.AddTag(GameTag.TagType.Scaler);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
    }
}
