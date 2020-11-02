using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentJackalEnemyCard : GameUnitCard
{
    public ContentJackalEnemyCard()
    {
        m_unit = new ContentJackalEnemy(null);

        InitEnemyCard();
    }
}