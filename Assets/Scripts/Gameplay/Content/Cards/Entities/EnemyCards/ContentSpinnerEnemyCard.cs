using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSpinnerEnemyCard : GameCardEntityBase
{
    public ContentSpinnerEnemyCard()
    {
        m_entity = new ContentSpinnerEnemy(null);

        InitEnemyCard();
    }
}