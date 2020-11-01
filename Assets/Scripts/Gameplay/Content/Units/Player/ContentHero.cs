using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHero : GameUnit
{
    public ContentHero()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.4f, 0);

        m_maxHealth = 50;
        m_maxStamina = 6;
        m_staminaRegen = 4;
        m_power = 6;

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;

        AddKeyword(new GameEnrageKeyword(new GameGainStatsAction(this, 1, 0)), false);
        AddKeyword(new GameMomentumKeyword(new GameHealAction(this, 5)), false);
        AddKeyword(new GameVictoriousKeyword(new GameGainResourceAction(new GameWallet(15))), false);

        m_name = "Hero";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }
}