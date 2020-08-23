using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenSentinelCard : GameCardEntityBase
{
    public ContentElvenSentinelCard()
    {
        m_entity = new ContentElvenSentinel();

        FillBasicData();

        m_playDesc = "A young marksman.  With training, he could be fierce!";
        m_typeline = "Summon - Elf";
        m_cost = 2;
    }
}
