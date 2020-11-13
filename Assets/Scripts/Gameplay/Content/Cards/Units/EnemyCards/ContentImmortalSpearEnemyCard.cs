using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentImmortalSpearEnemyCard : GameUnitCard
{
    public ContentImmortalSpearEnemyCard()
    {
        m_unit = new ContentImmortalSpearEnemy(null);

        InitEnemyCard();
    }
}