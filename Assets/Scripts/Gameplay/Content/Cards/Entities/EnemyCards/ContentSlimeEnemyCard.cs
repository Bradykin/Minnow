using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSlimeEnemyCard : GameCardEntityBase
{
    public ContentSlimeEnemyCard()
    {
        m_entity = new ContentSlimeEnemy(null);

        InitEnemyCard();
    }
}