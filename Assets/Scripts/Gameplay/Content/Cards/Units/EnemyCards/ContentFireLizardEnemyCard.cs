using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFireLizardEnemyCard : GameUnitCard
{
    public ContentFireLizardEnemyCard()
    {
        m_unit = new ContentFireLizardEnemy(null);

        InitEnemyCard();
    }
}