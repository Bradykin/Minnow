using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemonSkyterrorEnemyCard : GameUnitCard
{
    public ContentDemonSkyterrorEnemyCard()
    {
        m_unit = new ContentDemonSkyterrorEnemy(null);

        InitEnemyCard();
    }
}