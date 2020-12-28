using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ContentUndeadMammoth : GameUnit
{
    private int m_powerBuff = 3;
    private int m_healthBuff = 7;

    public ContentUndeadMammoth() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0.3f, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_name = "Undead Mammoth";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.SlamHeavy;

        AddKeyword(new GameDeathKeyword(new GameReturnToDeckBuffedAction(this, m_powerBuff, m_healthBuff)), true, false);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 12;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 4;
    }
}