using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDarkWarriorEnemyCard : GameUnitCard
{
    public ContentDarkWarriorEnemyCard()
    {
        m_unit = new ContentDarkWarriorEnemy(null);

        InitEnemyCard();
    }
}