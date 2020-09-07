using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRaptorCard : GameCardEntityBase
{
    public ContentRaptorCard()
    {
        m_entity = new ContentRaptor();

        FillBasicData();

        m_playDesc = "Rawk.  Rawwwwk!";
        m_cost = 1;
    }
}
