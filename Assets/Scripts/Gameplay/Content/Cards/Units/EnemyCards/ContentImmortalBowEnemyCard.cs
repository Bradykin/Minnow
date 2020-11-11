using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentImmortalBowEnemyCard : GameUnitCard
{
    public ContentImmortalBowEnemyCard()
    {
        m_unit = new ContentImmortalBowEnemy(null);

        InitEnemyCard();
    }
}