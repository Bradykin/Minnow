using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTreantMageEnemyCard : GameUnitCard
{
    public ContentTreantMageEnemyCard()
    {
        m_unit = new ContentTreantMageEnemy(null);

        InitEnemyCard();
    }
}