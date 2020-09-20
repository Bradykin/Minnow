using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDevourerCard : GameCardEntityBase
{
    public ContentDevourerCard()
    {
        m_entity = new ContentDevourer();

        FillBasicData();

        m_playDesc = "Vicious bites and a wild massacre mark its passage.";
        m_cost = 2;
    }
}
