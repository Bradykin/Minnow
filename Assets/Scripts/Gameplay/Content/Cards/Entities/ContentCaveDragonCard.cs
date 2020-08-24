using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCaveDragonCard : GameCardEntityBase
{
    public ContentCaveDragonCard()
    {
        m_entity = new ContentCaveDragonEntity();

        FillBasicData();

        m_playDesc = "While stunted by growing in a cave; this dragon is still feirce!";
        m_cost = 4;
    }
}
