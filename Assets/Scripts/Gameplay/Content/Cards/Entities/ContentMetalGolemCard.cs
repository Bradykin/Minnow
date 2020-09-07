using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMetalGolemCard : GameCardEntityBase
{
    public ContentMetalGolemCard()
    {
        m_entity = new ContentMetalGolem();

        FillBasicData();

        m_playDesc = "Does it look hungry to anyone else?";
        m_cost = 3;
    }
}
