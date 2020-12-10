using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowTrollEnemyCard : GameUnitCard
{
    public ContentSnowTrollEnemyCard()
    {
        m_unit = new ContentSnowTrollEnemy(null);

        InitEnemyCard();
    }
}