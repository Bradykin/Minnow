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

        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
    }

    public override string GetDesc()
    {
        return base.GetDesc() + "\nTotem of the wolf turn: " + GameHelper.GetPlayer().m_totemOfTheWolfTurn;
    }
}
