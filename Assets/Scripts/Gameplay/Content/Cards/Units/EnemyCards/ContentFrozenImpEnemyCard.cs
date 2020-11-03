using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFrozenImpEnemyCard : GameUnitCard
{
    public ContentFrozenImpEnemyCard()
    {
        m_unit = new ContentFrozenImpEnemy(null);

        InitEnemyCard();
    }
}