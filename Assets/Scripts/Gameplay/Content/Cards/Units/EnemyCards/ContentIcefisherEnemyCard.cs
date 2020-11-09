using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentIcefisherEnemyCard : GameUnitCard
{
    public ContentIcefisherEnemyCard()
    {
        m_unit = new ContentIcefisherEnemy(null);

        InitEnemyCard();
    }
}