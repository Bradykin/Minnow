using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLordOfWinterEnemyCard : GameUnitCard
{
    public ContentLordOfWinterEnemyCard()
    {
        m_unit = new ContentLordOfWinterEnemy(null);

        InitEnemyCard();
    }
}