using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPhoenixEnemyCard : GameUnitCard
{
    public ContentPhoenixEnemyCard()
    {
        m_unit = new ContentPhoenixEnemy(null);

        InitEnemyCard();
    }
}