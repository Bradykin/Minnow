using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentUndeadForestMonsterEnemyCard : GameUnitCard
{
    public ContentUndeadForestMonsterEnemyCard()
    {
        m_unit = new ContentUndeadForestMonsterEnemy(null);

        InitEnemyCard();
    }
}