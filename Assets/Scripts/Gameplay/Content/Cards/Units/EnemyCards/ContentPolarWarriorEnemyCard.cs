using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPolarWarriorEnemyCard : GameUnitCard
{
    public ContentPolarWarriorEnemyCard()
    {
        m_unit = new ContentPolarWarriorEnemy(null);

        InitEnemyCard();
    }
}