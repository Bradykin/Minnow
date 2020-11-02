using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCrumblingAncientEnemyCard : GameUnitCard
{
    public ContentCrumblingAncientEnemyCard()
    {
        m_unit = new ContentCrumblingAncientEnemy(null);

        InitEnemyCard();
    }
}