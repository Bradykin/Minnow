using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentImmortalShieldEnemyCard : GameUnitCard
{
    public ContentImmortalShieldEnemyCard()
    {
        m_unit = new ContentImmortalShieldEnemy(null);

        InitEnemyCard();
    }
}