using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAngryBirdEnemyCard : GameCardEntityBase
{
    public ContentAngryBirdEnemyCard()
    {
        m_entity = new ContentAngryBirdEnemy(null);

        InitEnemyCard();
    }
}