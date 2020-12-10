using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGoblinShamanEnemyCard : GameUnitCard
{
    public ContentGoblinShamanEnemyCard()
    {
        m_unit = new ContentGoblinShamanEnemy(null);

        InitEnemyCard();
    }
}