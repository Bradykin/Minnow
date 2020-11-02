using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentScorchingSerpentEnemyCard : GameUnitCard
{
    public ContentScorchingSerpentEnemyCard()
    {
        m_unit = new ContentScorchingSerpentEnemy(null);

        InitEnemyCard();
    }
}