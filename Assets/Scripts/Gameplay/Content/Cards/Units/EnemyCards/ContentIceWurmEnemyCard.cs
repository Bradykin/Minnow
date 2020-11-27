using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentIceWurmEnemyCard : GameUnitCard
{
    public ContentIceWurmEnemyCard()
    {
        m_unit = new ContentIceWurmEnemy(null);

        InitEnemyCard();
    }
}