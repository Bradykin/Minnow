using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCharybdisEnemyCard : GameUnitCard
{
    public ContentCharybdisEnemyCard()
    {
        m_unit = new ContentCharybdisEnemy(null);

        InitEnemyCard();
    }
}