using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLordOfEruptionsEnemyCard : GameUnitCard
{
    public ContentLordOfEruptionsEnemyCard()
    {
        m_unit = new ContentLordOfEruptionsEnemy(null);

        InitEnemyCard();
    }
}