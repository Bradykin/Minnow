using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOrcShamanEnemyCard : GameUnitCardBase
{
    public ContentOrcShamanEnemyCard()
    {
        m_unit = new ContentOrcShamanEnemy(null);

        InitEnemyCard();
    }
}