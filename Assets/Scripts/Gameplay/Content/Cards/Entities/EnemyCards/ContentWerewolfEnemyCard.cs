﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWerewolfEnemyCard : GameUnitCardBase
{
    public ContentWerewolfEnemyCard()
    {
        m_unit = new ContentWerewolfEnemy(null);

        InitEnemyCard();
    }
}