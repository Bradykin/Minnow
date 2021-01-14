using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTelloAltar : GameAltar
{
    public ContentTelloAltar(GameTile tile)
    {
        m_name = "Tello";
        m_eventDesc = "An altar of Tello.  Behold the power of the eye. (DOES NOTHING)"; //nmartino - Rework this
        m_tile = tile;
        m_rarity = GameRarity.Special;

        m_altarRelic = new ContentEyeOfTelloRelic();

        Init();
    }

    public override void LateInit()
    {
        m_optionOne = new GameEventTakeSpecificRelicOption(m_altarRelic);

        WorldGridManager.Instance.ClearAltars();

        base.LateInit();
    }
}
