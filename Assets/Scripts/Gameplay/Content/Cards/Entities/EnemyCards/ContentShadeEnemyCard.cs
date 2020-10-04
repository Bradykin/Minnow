using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShadeEnemyCard : GameUnitCard
{
    public ContentShadeEnemyCard()
    {
        m_unit = new ContentShadeEnemy(null);

        InitEnemyCard();
    }
}