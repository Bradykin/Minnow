using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGoblinCard : GameCardEntityBase
{
    public GameGoblinCard()
    {
        m_entity = new GameGoblinEntity();

        FillBasicData();

        m_playDesc = "A dubious goblin joins the fray on your side!  Yay...";
        m_typeline = "Summon - Goblin";
        m_cost = 1;
    }
}
