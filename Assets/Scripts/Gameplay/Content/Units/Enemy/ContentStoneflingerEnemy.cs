﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStoneflingerEnemy : GameEnemyUnit
{
    public ContentStoneflingerEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 40;
        m_maxStamina = 4;
        m_staminaRegen = 2;
        m_attack = 15;
        m_attackSFX = AudioHelper.SlamHeavy;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Stoneflinger";
        
        m_desc = "";

        AddKeyword(new GameRangeKeyword(2), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_desc = "If this unit attacks a building or attacks a unit defending a building, it deals double damage.";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int GetDamageToDealTo(GameUnit target)
    {
        int damageAmount = base.GetDamageToDealTo(target);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            if (target.GetGameTile().HasBuilding() && target.GetTeam() == target.GetGameTile().GetBuilding().GetTeam())
            {
                damageAmount *= 2;
            }
        }

        return damageAmount;
    }

    public override int GetDamageToDealTo(GameBuildingBase target)
    {
        return base.GetDamageToDealTo(target) * 2;
    }
}