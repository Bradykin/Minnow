using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentIcebladeWarriorEnemyCard : GameUnitCard
{
    public ContentIcebladeWarriorEnemyCard()
    {
        m_unit = new ContentIcebladeWarriorEnemy(null);

        InitEnemyCard();
    }
}