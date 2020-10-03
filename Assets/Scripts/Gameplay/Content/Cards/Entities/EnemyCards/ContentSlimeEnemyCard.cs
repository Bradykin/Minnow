using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSlimeEnemyCard : GameUnitCardBase
{
    public ContentSlimeEnemyCard()
    {
        m_unit = new ContentSlimeEnemy(null);

        InitEnemyCard();
    }
}