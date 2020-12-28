using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEnergyConstruct : GameUnit
{
    private int m_powerGain = 8;
    private int m_staminaGain = 4;

    public ContentEnergyConstruct() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;

        m_name = "Energy Construct";
        m_typeline = Typeline.Creation;
        m_desc = $"Gets +{m_powerGain}/+0 and +{m_staminaGain} stamina regen for each unspent energy.\n";
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.MetalClangAttack;

        LateInit();
    }

    public override int GetPower()
    {
        int returnVal = base.GetPower();

        if (m_gameTile == null)
        {
            return returnVal;
        }

        if (GameHelper.IsUnitInWorld(this))
        {
            returnVal += GameHelper.GetPlayer().GetCurEnergy() * m_powerGain;
        }

        return returnVal;
    }

    public override int GetStaminaRegen()
    {
        int returnVal = base.GetStaminaRegen();

        if (m_gameTile == null)
        {
            return returnVal;
        }

        if (GameHelper.IsUnitInWorld(this))
        {
            returnVal += GameHelper.GetPlayer().GetCurEnergy() * m_staminaGain;
        }

        return returnVal;
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 40;
        m_maxStamina = 6;
        m_staminaRegen = 0;
        m_power = 5;
    }
}