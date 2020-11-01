using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentVolcanoGolemEnemyCard : GameUnitCard
{
    public ContentVolcanoGolemEnemyCard()
    {
        m_unit = new ContentVolcanoGolemEnemy(null);

        InitEnemyCard();
    }
}