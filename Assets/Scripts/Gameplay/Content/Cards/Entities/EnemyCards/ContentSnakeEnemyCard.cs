using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnakeEnemyCard : GameUnitCardBase
{
    public ContentSnakeEnemyCard()
    {
        m_unit = new ContentSnakeEnemy(null);

        InitEnemyCard();
    }
}