using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentImmortalBladeEnemyCard : GameUnitCard
{
    public ContentImmortalBladeEnemyCard()
    {
        m_unit = new ContentImmortalBladeEnemy(null);

        InitEnemyCard();
    }
}