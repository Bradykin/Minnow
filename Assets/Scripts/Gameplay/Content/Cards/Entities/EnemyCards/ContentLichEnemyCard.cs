using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLichEnemyCard : GameCardEntityBase
{
    public ContentLichEnemyCard()
    {
        m_entity = new ContentLichEnemy(null);

        InitEnemyCard();
    }
}