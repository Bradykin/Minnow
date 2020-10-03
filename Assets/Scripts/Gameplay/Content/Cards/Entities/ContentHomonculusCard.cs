﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHomonculusCard : GameUnitCardBase
{
    public ContentHomonculusCard()
    {
        m_unit = new ContentHomonculus();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.StaminaRegen);
    }
}
