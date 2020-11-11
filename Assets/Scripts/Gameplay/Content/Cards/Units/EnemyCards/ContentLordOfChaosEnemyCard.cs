using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLordOfChaosEnemyCard : GameUnitCard
{
    public ContentLordOfChaosEnemyCard()
    {
        m_unit = new ContentLordOfChaosEnemy(null);

        InitEnemyCard();
    }
}