using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAngryTreantEnemyCard : GameUnitCard
{
    public ContentAngryTreantEnemyCard()
    {
        m_unit = new ContentAngryTreantEnemy(null);

        InitEnemyCard();
    }
}