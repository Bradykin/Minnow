using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfShivcasterCard : GameCardEntityBase
{
    public ContentDwarfShivcasterCard()
    {
        m_entity = new ContentDwarfShivcaster();

        FillBasicData();

        m_playDesc = "When did magic become shiv related? Your guess is as good as mine.";
        m_cost = 2;
    }
}
