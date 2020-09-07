using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemonSoldierCard : GameCardEntityBase
{
    public ContentDemonSoldierCard()
    {
        m_entity = new ContentDemonSoldier();

        FillBasicData();

        m_playDesc = "The flapping of wings high above is often the final sound mortals hear.";
        m_cost = 2;
    }
}
