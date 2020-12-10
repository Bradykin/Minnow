using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGiantBrawlerEnemyCard : GameUnitCard
{
    public ContentGiantBrawlerEnemyCard()
    {
        m_unit = new ContentGiantBrawlerEnemy(null);

        InitEnemyCard();
    }
}