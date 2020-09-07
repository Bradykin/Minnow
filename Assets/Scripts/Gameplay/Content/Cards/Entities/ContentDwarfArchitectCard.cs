using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfArchitectCard : GameCardEntityBase
{
    public ContentDwarfArchitectCard()
    {
        m_entity = new ContentDwarfArchitect();

        FillBasicData();

        m_playDesc = "Hammers ring through the air as the wheels of production are restored.";
        m_cost = 1;
    }
}
