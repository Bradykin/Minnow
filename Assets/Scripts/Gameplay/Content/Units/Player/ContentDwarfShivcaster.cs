using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfShivcaster : GameUnit
{
    public ContentDwarfShivcaster()
    {
        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Dwarf Shivcaster";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        AddKeyword(new GameSpellcraftKeyword(new GameShivNearbyAction(this, 2, 2)), true, false);
        AddKeyword(new GameRangeKeyword(2), true, false);
        AddKeyword(new GameShivKeyword(), true, false);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 16;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_power = 2;
    }
}