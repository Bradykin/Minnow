﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCyclopsCard : GameUnitCard
{
    public ContentCyclopsCard()
    {
        m_unit = new ContentCyclops();

        m_cost = 1;
        m_playerUnlockLevel = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}