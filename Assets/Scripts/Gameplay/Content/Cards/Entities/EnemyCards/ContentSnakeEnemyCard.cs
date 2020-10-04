using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnakeEnemyCard : GameUnitCard
{
    public ContentSnakeEnemyCard()
    {
        m_unit = new ContentSnakeEnemy(null);

        InitEnemyCard();
    }
}