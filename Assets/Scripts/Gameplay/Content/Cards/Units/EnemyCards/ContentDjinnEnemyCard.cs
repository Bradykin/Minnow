using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDjinnEnemyCard : GameUnitCard
{
    public ContentDjinnEnemyCard()
    {
        m_unit = new ContentDjinnEnemy(null);

        InitEnemyCard();
    }
}