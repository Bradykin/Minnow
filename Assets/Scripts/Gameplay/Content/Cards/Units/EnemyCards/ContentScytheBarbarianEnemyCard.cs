using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentScytheBarbarianEnemyCard : GameUnitCard
{
    public ContentScytheBarbarianEnemyCard()
    {
        m_unit = new ContentScytheBarbarianEnemy(null);

        InitEnemyCard();
    }
}