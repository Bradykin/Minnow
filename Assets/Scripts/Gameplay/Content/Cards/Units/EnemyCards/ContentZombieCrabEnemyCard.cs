using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentZombieCrabEnemyCard : GameUnitCard
{
    public ContentZombieCrabEnemyCard()
    {
        m_unit = new ContentZombieCrabEnemy(null);

        InitEnemyCard();
    }
}