using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRangerCard : GameCardEntityBase
{
    public ContentRangerCard()
    {
        m_entity = new ContentRanger();

        FillBasicData();

        m_playDesc = "He defends the forests.";
        m_cost = 1;
    }
}
