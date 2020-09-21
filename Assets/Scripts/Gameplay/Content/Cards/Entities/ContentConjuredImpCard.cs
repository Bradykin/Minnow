using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentConjuredImpCard : GameCardEntityBase
{
    public ContentConjuredImpCard()
    {
        m_entity = new ContentConjuredImp();

        FillBasicData();

        m_playDesc = "With a poof and a giggle, a mighty imp appears.";
        m_cost = 0;
    }
}
