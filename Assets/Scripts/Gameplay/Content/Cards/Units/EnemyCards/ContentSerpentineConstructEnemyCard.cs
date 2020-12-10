using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSerpentineConstructEnemyCard : GameUnitCard
{
    public ContentSerpentineConstructEnemyCard()
    {
        m_unit = new ContentSerpentineConstructEnemy(null);

        InitEnemyCard();
    }
}