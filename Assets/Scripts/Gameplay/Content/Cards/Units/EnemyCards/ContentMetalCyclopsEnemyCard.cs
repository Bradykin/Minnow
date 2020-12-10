using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMetalCyclopsEnemyCard : GameUnitCard
{
    public ContentMetalCyclopsEnemyCard()
    {
        m_unit = new ContentMetalCyclopsEnemy(null);

        InitEnemyCard();
    }
}