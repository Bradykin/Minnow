using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDarkWarriorEnemyCard : GameCardEntityBase
{
    public ContentDarkWarriorEnemyCard()
    {
        m_entity = new ContentDarkWarriorEnemy(null);

        InitEnemyCard();
    }
}