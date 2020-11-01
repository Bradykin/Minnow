using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStoneGolem : GameUnit
{
    private GameDamageReductionKeyword m_drKeyword;

    public ContentStoneGolem()
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.4f, 0);

        m_maxHealth = 20;
        m_maxStamina = 3;
        m_staminaRegen = 1;
        m_power = 1;

        m_desc = "When at maximum stamina, Stone Golem has Damage Reduction 4.\n";

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Stone Golem";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        m_drKeyword = new GameDamageReductionKeyword(4);

        LateInit();
    }

    public override void GainStamina(int toGain, bool isRegen = false)
    {
        base.GainStamina(toGain, isRegen);

        if (m_curStamina == m_maxStamina)
        {
            if (GetKeyword<GameDamageReductionKeyword>() == null)
            {
                AddKeyword(m_drKeyword, false);
            }
        }
    }

    public override void SpendStamina(int toSpend)
    {
        base.SpendStamina(toSpend);

        if (m_curStamina < m_maxStamina)
        {
            if (GetKeyword<GameDamageReductionKeyword>() != null)
            {
                SubtractKeyword(m_drKeyword);
            }
        }
    }
}