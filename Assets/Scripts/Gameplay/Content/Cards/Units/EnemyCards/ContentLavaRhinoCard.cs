using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLavaRhinoCard : GameUnitCard
{
    public ContentLavaRhinoCard()
    {
        m_unit = new ContentLavaRhinoEnemy(null);

        InitEnemyCard();
    }
}