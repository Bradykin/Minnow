using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLizardmanEnemyCard : GameCardEntityBase
{
    public ContentLizardmanEnemyCard()
    {
        m_entity = new ContentLizardmanEnemy(null);

        InitEnemyCard();
    }
}