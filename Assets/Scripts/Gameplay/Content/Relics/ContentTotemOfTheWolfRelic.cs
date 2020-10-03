using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTotemOfTheWolfRelic : GameRelic
{
    public ContentTotemOfTheWolfRelic()
    {
        m_name = "Totem of the Wolf";
        m_desc = "One turn per wave, enter the full moon. Allied units gain double Stamina regen and attacking costs 1 less Stamina (minimum of 1).";
        m_rarity = GameRarity.Uncommon;

        LateInit();
    }

    public override string GetDesc()
    {
        return base.GetDesc() + "\nTotem of the wolf turn: " + Globals.m_totemOfTheWolfTurn;
    }
}
