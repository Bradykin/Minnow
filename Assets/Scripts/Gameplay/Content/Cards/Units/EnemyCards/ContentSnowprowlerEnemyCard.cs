using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowprowlerEnemyCard : GameUnitCard
{
    public ContentSnowprowlerEnemyCard()
    {
        m_unit = new ContentSnowprowlerEnemy(null);

        InitEnemyCard();
    }
}