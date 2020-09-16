using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWildfolkCard : GameCardEntityBase
{
    public ContentWildfolkCard()
    {
        m_entity = new ContentWildfolk();

        FillBasicData();

        m_playDesc = "Poof!  Wiz!  Zabang!";
        m_cost = 2;
    }
}
