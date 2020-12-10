using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSwampwalkerEnemyCard : GameUnitCard
{
    public ContentSwampwalkerEnemyCard()
    {
        m_unit = new ContentSwampwalkerEnemy(null);

        InitEnemyCard();
    }
}