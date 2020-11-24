using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSpiralSerpentEnemyCard : GameUnitCard
{
    public ContentSpiralSerpentEnemyCard()
    {
        m_unit = new ContentSpiralSerpentEnemy(null);

        InitEnemyCard();
    }
}