using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentZombieEnemyCard : GameUnitCard
{
    public ContentZombieEnemyCard()
    {
        m_unit = new ContentZombieEnemy(null);

        InitEnemyCard();
    }
}

