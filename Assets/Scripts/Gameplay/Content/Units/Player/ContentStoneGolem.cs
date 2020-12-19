using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStoneGolem : GameUnit
{
    private bool m_staminaAdded;

    public ContentStoneGolem()
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.4f, 0);

        m_desc = "When at maximum stamina, Stone Golem has Damage Reduction equal to its maximum stamina.\n";

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Stone Golem";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.SlamHeavy;

        LateInit();
    }

    public override GameDamageReductionKeyword GetDamageReductionKeyword()
    {
        GameDamageReductionKeyword toReturn = new GameDamageReductionKeyword(0);

        if (base.GetDamageReductionKeyword() != null)
        {
            toReturn.AddKeyword(base.GetDamageReductionKeyword());
        }

        if (GameHelper.IsUnitInWorld(this))
        {
            if (GetCurStamina() == GetMaxStamina())
            {
                toReturn.AddKeyword(new GameDamageReductionKeyword(GetMaxStamina()));
            }
        }

        if (toReturn.m_damageReduction == 0)
        {
            toReturn = null;
        }

        return toReturn;
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 50;
        m_maxStamina = 3;
        m_staminaRegen = 2;
        m_power = 0;
    }
}