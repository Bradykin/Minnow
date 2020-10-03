using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentYetiEnemyCard : GameUnitCardBase
{
    public ContentYetiEnemyCard()
    {
        m_unit = new ContentYetiEnemy(null);

        InitEnemyCard();
    }
}