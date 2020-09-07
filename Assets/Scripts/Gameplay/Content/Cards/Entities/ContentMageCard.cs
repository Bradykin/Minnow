using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMageCard : GameCardEntityBase
{
    public ContentMageCard()
    {
        m_entity = new ContentMage();

        FillBasicData();

        m_playDesc = "Still learning, but soon a force to be reckoned with!";
        m_cost = 1;
    }
}
