using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNorthernBerserkerEnemyCard : GameUnitCard
{
    public ContentNorthernBerserkerEnemyCard()
    {
        m_unit = new ContentNorthernBerserkerEnemy(null);

        InitEnemyCard();
    }
}