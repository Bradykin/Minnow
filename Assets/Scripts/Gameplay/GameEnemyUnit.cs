using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemyUnit : GameUnit
{
    public AIGameEnemyUnit m_AIGameEnemyUnit;
    public GameOpponent m_gameOpponentController;

    public bool m_isElite;
    public bool m_isBoss;

    public int m_experienceAmount = 5;

    public GameEnemyUnit(GameOpponent gameOpponent)
    {
        m_AIGameEnemyUnit = new AIGameEnemyUnit(this);
        m_gameOpponentController = gameOpponent;
    }

    protected override void LateInit()
    {
        base.LateInit();

        m_startWithMaxStamina = true;

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.EnemyStrength))
        {
            m_maxHealth = Mathf.FloorToInt(m_maxHealth * 1.5f);
        }

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.EnemyStrength))
        {
            m_power = Mathf.FloorToInt(m_power * 1.5f);
        }

        SetHealthStaminaValues();

        m_typeline = Typeline.Monster;

        if (m_isBoss || m_isElite)
        {
            m_usesBigTooltip = true;
        }
    }

    //============================================================================================================//


    public override void SpellCast(GameCard.Target targetType, GameTile targetTile)
    {
        GameSpellcraftKeyword spellcraftKeyword = GetSpellcraftKeyword();

        if (spellcraftKeyword == null)
        {
            return;
        }

        if (Constants.UseLocationalSpellcraft)
        {
            if (targetType == GameCard.Target.None)
            {
                return;
            }

            if (targetTile == null)
            {
                Debug.LogError("Spellcast that isn't target none received null target tile");
                return;
            }

            int distanceBetween = WorldGridManager.Instance.GetPathLength(GetGameTile(), targetTile, true, false, true);
            if (distanceBetween > GameSpellcraftKeyword.m_spellcraftRange)
            {
                return;
            }
        }

        spellcraftKeyword.DoAction();
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        base.Die(canRevive, damageType);

        if (!m_isDead)
        {
            return;
        }

        if (m_isElite)
        {
            GameHelper.GetGameController().AddEliteExp(m_experienceAmount);
        }
        else
        {
            GameHelper.GetGameController().AddKillExp(m_experienceAmount);
        }
        m_gameOpponentController.m_controlledUnits.Remove(this);
    }
}
