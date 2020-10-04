using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSlimeEnemyCard : GameUnitCard
{
    public ContentSlimeEnemyCard()
    {
        m_unit = new ContentSlimeEnemy(null);

        InitEnemyCard();
    }
}