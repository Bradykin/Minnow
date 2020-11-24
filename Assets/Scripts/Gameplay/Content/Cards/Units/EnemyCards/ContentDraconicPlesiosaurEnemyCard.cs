using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDraconicPlesiosaurEnemyCard : GameUnitCard
{
    public ContentDraconicPlesiosaurEnemyCard()
    {
        m_unit = new ContentDraconicPlesiosaurEnemy(null);

        InitEnemyCard();
    }
}