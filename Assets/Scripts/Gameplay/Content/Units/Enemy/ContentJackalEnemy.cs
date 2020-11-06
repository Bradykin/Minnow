using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentJackalEnemy : GameEnemyUnit
{
    public int m_basePower = 16;
    public int m_baseMaxHealth = 45;
    public int m_range = 2;
    
    public ContentJackalEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = m_baseMaxHealth;
        m_maxStamina = 5;
        m_staminaRegen = 5;
        m_power = m_basePower;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_minWave = 5;
        m_maxWave = 5;

        m_name = "Jackal";
        m_desc = "";

        AddKeyword(new GameVictoriousKeyword(new GameGainStatsAction(this, 10, 10)), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_desc = $"When this unit dies, it gives all allied units in {m_range} range stats equal to the amount of additional stats it has received (+{m_power - m_basePower}/{m_maxHealth - m_baseMaxHealth})\n";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override string GetDesc()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            return $"When this unit dies, it gives all allied units in {m_range} range stats equal to the amount of additional stats it has received (+{m_power - m_basePower}/+{m_maxHealth - m_baseMaxHealth})\n";
        }
        
        return base.GetDesc();
    }

    protected override void LateInit()
    {
        base.LateInit();


        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.EnemyStrength))
        {
            m_baseMaxHealth = Mathf.FloorToInt(m_baseMaxHealth * 1.5f);
        }

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.EnemyStrength))
        {
            m_basePower = Mathf.FloorToInt(m_basePower * 1.5f);
        }
    }

    public override void Die(bool canRevive = true)
    {
        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_range);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            if (surroundingTiles[i].IsOccupied() && !surroundingTiles[i].m_occupyingUnit.m_isDead && surroundingTiles[i].m_occupyingUnit.GetTeam() == GetTeam())
            {
                surroundingTiles[i].m_occupyingUnit.AddStats(Mathf.Max(0, m_power - m_basePower), Mathf.Max(m_maxHealth - m_baseMaxHealth));
            }
        }

        base.Die(canRevive);
    }
}