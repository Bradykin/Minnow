using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOrcShamanEnemyCard : GameUnitCard
{
    public ContentOrcShamanEnemyCard()
    {
        m_unit = new ContentOrcShamanEnemy(null);

        InitEnemyCard();
    }
}