using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentArcticSkyfoxEnemyCard : GameUnitCard
{
    public ContentArcticSkyfoxEnemyCard()
    {
        m_unit = new ContentArcticSkyfoxEnemy(null);

        InitEnemyCard();
    }
}