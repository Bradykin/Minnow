using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGoblinWarriorEnemyCard : GameUnitCard
{
    public ContentGoblinWarriorEnemyCard()
    {
        m_unit = new ContentGoblinWarriorEnemy(null);

        InitEnemyCard();
    }
}