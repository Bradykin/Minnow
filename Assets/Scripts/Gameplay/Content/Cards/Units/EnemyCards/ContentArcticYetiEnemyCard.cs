using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentArcticYetiEnemyCard : GameUnitCard
{
    public ContentArcticYetiEnemyCard()
    {
        m_unit = new ContentArcticYetiEnemy(null);

        InitEnemyCard();
    }
}