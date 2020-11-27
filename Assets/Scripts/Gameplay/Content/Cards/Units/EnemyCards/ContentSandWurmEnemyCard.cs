using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSandWurmEnemyCard : GameUnitCard
{
    public ContentSandWurmEnemyCard()
    {
        m_unit = new ContentSandWurmEnemy(null);

        InitEnemyCard();
    }
}