using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrasperCard : GameCardEntityBase
{
    public ContentGrasperCard()
    {
        m_entity = new ContentGrasper();

        FillBasicData();

        m_playDesc = "Sluuuurp!";
        m_cost = 1;
    }
}
