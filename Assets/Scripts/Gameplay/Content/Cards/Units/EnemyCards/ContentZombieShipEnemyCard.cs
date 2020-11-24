using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentZombieShipEnemyCard : GameUnitCard
{
    public ContentZombieShipEnemyCard()
    {
        m_unit = new ContentZombieShipEnemy(null);

        InitEnemyCard();
    }
}