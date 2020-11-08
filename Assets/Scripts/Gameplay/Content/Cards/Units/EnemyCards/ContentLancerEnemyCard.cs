using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLancerEnemyCard : GameUnitCard
{
    public ContentLancerEnemyCard()
    {
        m_unit = new ContentLancerEnemy(null);

        InitEnemyCard();
    }
}