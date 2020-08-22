using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInjuredTrollCard : GameCardEntityBase
{
    public GameInjuredTrollCard()
    {
        m_entity = new GameInjuredTrollEntity();

        FillBasicData();

        m_playDesc = "This troll may be injured for now, but it will soon be full strength and mighty!";
        m_typeline = "Summon - Troll";
        m_cost = 2;
    }
}
