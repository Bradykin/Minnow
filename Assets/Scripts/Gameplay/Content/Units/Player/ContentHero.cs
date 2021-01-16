using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHero : GameUnit
{
    public ContentHero() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.4f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;

        AddKeyword(new GameEnrageKeyword(new GameGainStatsAction(this, 3, 0)), true, false);
        AddKeyword(new GameMomentumKeyword(new GameHealAction(this, 5)), true, false);
        AddKeyword(new GameVictoriousKeyword(new GameGainGoldAction(15)), true, false);

        m_name = "Hero";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.SwordHeavy;

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 50;
        m_maxStamina = 6;
        m_staminaRegen = 4;
        m_attack = 6;
    }
}