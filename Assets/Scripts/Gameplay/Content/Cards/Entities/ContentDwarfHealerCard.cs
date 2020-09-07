using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfHealerCard : GameCardEntityBase
{
    public ContentDwarfHealerCard()
    {
        m_entity = new ContentDwarfHealer();

        FillBasicData();

        m_playDesc = "She heals as quickly as she can; but the injured keep coming in.";
        m_cost = 1;
    }
}
