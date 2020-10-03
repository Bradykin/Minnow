using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentZombieEnemyCard : GameUnitCardBase
{
    public ContentZombieEnemyCard()
    {
        m_unit = new ContentZombieEnemy(null);

        InitEnemyCard();
    }
}

