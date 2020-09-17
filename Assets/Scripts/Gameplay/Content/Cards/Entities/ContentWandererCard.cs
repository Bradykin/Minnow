using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWandererCard : GameCardEntityBase
{
    public ContentWandererCard()
    {
        m_entity = new ContentWanderer();

        FillBasicData();

        m_playDesc = "He wanders in from a nearby land. With many shivs.";
        m_cost = 2;
    }
}
