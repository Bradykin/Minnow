﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrasperCard : GameUnitCard
{
    public ContentGrasperCard()
    {
        m_unit = new ContentGrasper();

        m_cost = 1;
        m_playerUnlockLevel = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.MaxStamina);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Tank);
    }
}