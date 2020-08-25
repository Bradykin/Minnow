using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNaturalScoutCard : GameCardEntityBase
{
    public ContentNaturalScoutCard()
    {
        m_entity = new ContentNaturalScout();

        FillBasicData();

        m_playDesc = "This thing is so quick; it's wild!";
        m_cost = 1;
    }
}
