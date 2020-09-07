using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShadowWarlockCard : GameCardEntityBase
{
    public ContentShadowWarlockCard()
    {
        m_entity = new ContentShadowWarlock();

        FillBasicData();

        m_playDesc = "DESC!";
        m_cost = 1;
    }
}
