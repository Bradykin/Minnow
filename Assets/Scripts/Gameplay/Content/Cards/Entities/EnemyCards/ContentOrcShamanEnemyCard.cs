using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOrcShamanEnemyCard : GameCardEntityBase
{
    public ContentOrcShamanEnemyCard()
    {
        m_entity = new ContentOrcShamanEnemy(null);

        InitEnemyCard();
    }
}