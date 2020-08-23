using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenRogueCard : GameCardEntityBase
{
    public ContentElvenRogueCard()
    {
        m_entity = new ContentElvenRogue();

        FillBasicData();

        m_playDesc = "A rogue only grows stronger with experience...";
        m_typeline = "Summon - Elf";
        m_cost = 1;
    }
}
