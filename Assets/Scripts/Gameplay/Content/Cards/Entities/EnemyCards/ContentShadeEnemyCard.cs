using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShadeEnemyCard : GameCardEntityBase
{
    public ContentShadeEnemyCard()
    {
        m_entity = new ContentShadeEnemy(null);

        InitEnemyCard();
    }
}