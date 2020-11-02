﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOverlordCard : GameUnitCard
{
    public ContentOverlordCard()
    {
        m_unit = new ContentOverlord();

        m_cost = 2;
        m_playerUnlockLevel = 4;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Explorer);
        m_tags.AddTag(GameTag.TagType.Scaler);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
    }
}