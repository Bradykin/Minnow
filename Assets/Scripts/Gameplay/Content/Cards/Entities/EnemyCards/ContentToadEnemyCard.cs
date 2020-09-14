using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentToadEnemyCard : GameCardEntityBase
{
    public ContentToadEnemyCard()
    {
        m_entity = new ContentToadEnemy(null);

        InitEnemyCard();
    }
}