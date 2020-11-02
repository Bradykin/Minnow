using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentToadEnemyCard : GameUnitCard
{
    public ContentToadEnemyCard()
    {
        m_unit = new ContentToadEnemy(null);

        InitEnemyCard();
    }
}