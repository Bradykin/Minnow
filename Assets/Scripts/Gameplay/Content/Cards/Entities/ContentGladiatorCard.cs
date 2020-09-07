using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGladiatorCard : GameCardEntityBase
{
    public ContentGladiatorCard()
    {
        m_entity = new ContentGladiator();

        FillBasicData();

        m_playDesc = "The last being to touch him died in an invisible flash of steel.";
        m_cost = 2;
    }
}
