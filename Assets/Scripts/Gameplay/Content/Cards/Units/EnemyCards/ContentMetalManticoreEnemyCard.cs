using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMetalManticoreEnemyCard : GameUnitCard
{
    public ContentMetalManticoreEnemyCard()
    {
        m_unit = new ContentMetalManticoreEnemy(null);

        InitEnemyCard();
    }
}