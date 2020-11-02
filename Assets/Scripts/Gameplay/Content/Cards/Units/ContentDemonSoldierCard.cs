﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemonSoldierCard : GameUnitCard
{
    public ContentDemonSoldierCard()
    {
        m_unit = new ContentDemonSoldier();

        m_cost = 3;
        m_playerUnlockLevel = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Explorer);
    }
}