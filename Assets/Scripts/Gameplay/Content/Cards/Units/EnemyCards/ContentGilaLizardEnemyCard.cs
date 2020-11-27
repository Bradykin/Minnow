using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGilaLizardEnemyCard : GameUnitCard
{
    public ContentGilaLizardEnemyCard()
    {
        m_unit = new ContentGilaLizardEnemy(null);

        InitEnemyCard();
    }
}