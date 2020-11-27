using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentIfritEnemyCard : GameUnitCard
{
    public ContentIfritEnemyCard()
    {
        m_unit = new ContentIfritEnemy(null);

        InitEnemyCard();
    }
}