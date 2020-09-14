using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentYetiEnemyCard : GameCardEntityBase
{
    public ContentYetiEnemyCard()
    {
        m_entity = new ContentYetiEnemy(null);

        InitEnemyCard();
    }
}