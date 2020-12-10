using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNightmareWorkerEnemyCard : GameUnitCard
{
    public ContentNightmareWorkerEnemyCard()
    {
        m_unit = new ContentNightmareWorkerEnemy(null);

        InitEnemyCard();
    }
}