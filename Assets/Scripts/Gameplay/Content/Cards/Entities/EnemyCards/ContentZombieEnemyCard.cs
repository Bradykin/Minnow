using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentZombieEnemyCard : GameCardEntityBase
{
    public ContentZombieEnemyCard()
    {
        m_entity = new ContentZombieEnemy(null);

        InitEnemyCard();
    }
}

