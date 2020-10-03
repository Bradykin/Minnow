using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLizardmanEnemyCard : GameUnitCardBase
{
    public ContentLizardmanEnemyCard()
    {
        m_unit = new ContentLizardmanEnemy(null);

        InitEnemyCard();
    }
}