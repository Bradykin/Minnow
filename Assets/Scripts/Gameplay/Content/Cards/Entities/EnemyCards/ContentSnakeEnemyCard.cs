using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnakeEnemyCard : GameCardEntityBase
{
    public ContentSnakeEnemyCard()
    {
        m_entity = new ContentSnakeEnemy(null);

        InitEnemyCard();
    }
}