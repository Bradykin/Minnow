using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFlameImpEnemyCard : GameUnitCard
{
    public ContentFlameImpEnemyCard()
    {
        m_unit = new ContentFlameImpEnemy(null);

        InitEnemyCard();
    }
}