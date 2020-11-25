using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLavaGoblinEnemyCard : GameUnitCard
{
    public ContentLavaGoblinEnemyCard()
    {
        m_unit = new ContentLavaGoblinEnemy(null);

        InitEnemyCard();
    }
}