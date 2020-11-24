using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRedDragonEnemyCard : GameUnitCard
{
    public ContentRedDragonEnemyCard()
    {
        m_unit = new ContentRedDragonEnemy(null);

        InitEnemyCard();
    }
}