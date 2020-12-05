using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDevourer : GameUnit
{
    public ContentDevourer()
    {
        m_team = Team.Player;
        m_rarity = GameRarity.Rare;

        AddKeyword(new GameVictoriousKeyword(new GameGainStatsPermanentAction(this, 1, 1)), true, false);

        m_name = "Devourer";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 20;
        m_maxStamina = 6;
        m_staminaRegen = 4;
        m_power = 5;
    }
}