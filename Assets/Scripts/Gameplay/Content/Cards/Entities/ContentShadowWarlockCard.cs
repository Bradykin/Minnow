using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShadowWarlockCard : GameCardEntityBase
{
    public ContentShadowWarlockCard()
    {
        m_entity = new ContentShadowWarlock();

        FillBasicData();

        m_playDesc = "Dark powers form a figure out of the shadows!";
        m_cost = 2;
    }
}
