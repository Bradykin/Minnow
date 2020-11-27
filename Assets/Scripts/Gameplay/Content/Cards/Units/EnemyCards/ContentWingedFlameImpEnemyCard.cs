using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWingedFlameImpEnemyCard : GameUnitCard
{
    public ContentWingedFlameImpEnemyCard()
    {
        m_unit = new ContentWingedFlameImpEnemy(null);

        InitEnemyCard();
    }
}