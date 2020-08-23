﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGoblinCard : GameCardEntityBase
{
    public ContentGoblinCard()
    {
        m_entity = new ContentGoblinEntity();

        FillBasicData();

        m_playDesc = "A dubious goblin joins the fray on your side!  Yay...";
        m_typeline = "Summon - Goblin";
        m_cost = 1;
    }
}