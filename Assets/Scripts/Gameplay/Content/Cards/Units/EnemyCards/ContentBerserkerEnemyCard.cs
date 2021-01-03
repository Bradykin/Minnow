using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBerserkerEnemyCard : GameUnitCard
{
    public ContentBerserkerEnemyCard()
    {
        m_unit = new ContentBerserkerEnemy(null);

        InitEnemyCard();
    }
}