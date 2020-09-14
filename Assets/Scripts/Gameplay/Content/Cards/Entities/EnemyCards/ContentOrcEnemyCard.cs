using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOrcEnemyCard : GameCardEntityBase
{
    public ContentOrcEnemyCard()
    {
        m_entity = new ContentOrcEnemy(null);

        InitEnemyCard();
    }
}