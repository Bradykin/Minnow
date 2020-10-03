using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOrcEnemyCard : GameUnitCardBase
{
    public ContentOrcEnemyCard()
    {
        m_unit = new ContentOrcEnemy(null);

        InitEnemyCard();
    }
}