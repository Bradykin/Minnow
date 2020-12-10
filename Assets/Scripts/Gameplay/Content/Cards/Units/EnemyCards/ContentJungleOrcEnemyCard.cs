using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentJungleOrcEnemyCard : GameUnitCard
{
    public ContentJungleOrcEnemyCard()
    {
        m_unit = new ContentJungleOrcEnemy(null);

        InitEnemyCard();
    }
}