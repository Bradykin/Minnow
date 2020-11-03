using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowWolfbeastEnemyCard : GameUnitCard
{
    public ContentSnowWolfbeastEnemyCard()
    {
        m_unit = new ContentSnowWolfbeastEnemy(null);

        InitEnemyCard();
    }
}