using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMobolaEnemyCard : GameCardEntityBase
{
    public ContentMobolaEnemyCard()
    {
        m_entity = new ContentMobolaEnemy(null);

        InitEnemyCard();
    }
}