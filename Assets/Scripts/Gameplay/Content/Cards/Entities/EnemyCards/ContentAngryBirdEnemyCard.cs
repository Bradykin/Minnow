using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAngryBirdEnemyCard : GameUnitCardBase
{
    public ContentAngryBirdEnemyCard()
    {
        m_unit = new ContentAngryBirdEnemy(null);

        InitEnemyCard();
    }
}