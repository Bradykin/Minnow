using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentImmortalBannerEnemyCard : GameUnitCard
{
    public ContentImmortalBannerEnemyCard()
    {
        m_unit = new ContentImmortalBannerEnemy(null);

        InitEnemyCard();
    }
}