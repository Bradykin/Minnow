using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCyclopsCard : GameCardEntityBase
{
    public ContentCyclopsCard()
    {
        m_entity = new ContentCyclops();

        FillBasicData();

        m_playDesc = "Slow and steady...";
        m_cost = 1;
    }
}
