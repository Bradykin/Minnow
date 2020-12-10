using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFailedExperimentEnemyCard : GameUnitCard
{
    public ContentFailedExperimentEnemyCard()
    {
        m_unit = new ContentFailedExperimentEnemy(null);

        InitEnemyCard();
    }
}