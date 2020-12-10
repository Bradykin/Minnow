using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBlueMageEnemyCard : GameUnitCard
{
    public ContentBlueMageEnemyCard()
    {
        m_unit = new ContentBlueMageEnemy(null);

        InitEnemyCard();
    }
}