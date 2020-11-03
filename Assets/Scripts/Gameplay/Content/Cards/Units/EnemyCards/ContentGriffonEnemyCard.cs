using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGriffonEnemyCard : GameUnitCard
{
    public ContentGriffonEnemyCard()
    {
        m_unit = new ContentGriffonEnemy(null);

        InitEnemyCard();
    }
}