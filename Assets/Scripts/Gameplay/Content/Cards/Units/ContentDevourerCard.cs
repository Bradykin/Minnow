﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDevourerCard : GameUnitCard
{
    public ContentDevourerCard()
    {
        m_unit = new ContentDevourer();

        m_cost = 2;
        m_playerUnlockLevel = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}