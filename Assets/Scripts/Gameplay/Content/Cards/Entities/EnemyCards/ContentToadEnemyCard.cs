using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentToadEnemyCard : GameUnitCardBase
{
    public ContentToadEnemyCard()
    {
        m_unit = new ContentToadEnemy(null);

        InitEnemyCard();
    }
}