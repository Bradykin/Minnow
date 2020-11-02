using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFlamesoulElementalEnemyCard : GameUnitCard
{
    public ContentFlamesoulElementalEnemyCard()
    {
        m_unit = new ContentFlamesoulElementalEnemy(null);

        InitEnemyCard();
    }
}