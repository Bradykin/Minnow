﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentInjuredTrollCard : GameUnitCard
{
    public ContentInjuredTrollCard()
    {
        m_unit = new ContentInjuredTroll();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Healing);
        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}