using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPirateCaptain : GameUnit
{
    private int m_statBoost = 50;
    private int m_rangeBoost = 2;

    public ContentPirateCaptain() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        AddKeyword(new GameWaterwalkKeyword(), true, false);

        m_name = "Pirate Captain";
        m_desc = $"When in water, gain +{m_statBoost}/+0 and +{m_rangeBoost} <b>Range</b>.\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override AudioClip GetAttackSFX()
    {
        if (m_gameTile != null)
        {
            if (m_gameTile.GetTerrain().IsWater())
            {
                return AudioHelper.BowHeavy;
            }
        }

        return AudioHelper.PunchLight;
    }

    public override int GetAttack()
    {
        int returnAttack = base.GetAttack();

        if (GameHelper.IsUnitInWorld(this))
        {
            if (m_gameTile.GetTerrain().IsWater())
            {
                returnAttack += m_statBoost;
            }
        }

        return returnAttack;
    }

    public override GameRangeKeyword GetRangeKeyword()
    {
        GameRangeKeyword toReturn = new GameRangeKeyword(0);

        if (base.GetRangeKeyword() != null)
        {
            toReturn.AddKeyword(base.GetRangeKeyword());
        }

        if (GameHelper.IsUnitInWorld(this))
        {
            if (m_gameTile.GetTerrain().IsWater())
            {
                toReturn.AddKeyword(new GameRangeKeyword(m_rangeBoost));
            }
        }

        if (toReturn.m_range == 0)
        {
            toReturn = null;
        }

        return toReturn;
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 20;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_attack = 0;
    }
}