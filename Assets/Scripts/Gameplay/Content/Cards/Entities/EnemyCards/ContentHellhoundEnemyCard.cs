using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHellhoundEnemyCard : GameUnitCard
{
    public ContentHellhoundEnemyCard()
    {
        m_unit = new ContentHellhoundEnemy(null);

        InitEnemyCard();
    }
}