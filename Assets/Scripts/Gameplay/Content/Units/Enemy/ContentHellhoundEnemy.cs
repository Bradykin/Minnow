using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHellhoundEnemy : GameEnemyUnit
{
    int m_effectRange = 4;
    int m_effectIncrease = 1;
    
    public ContentHellhoundEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 10;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_power = 3;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_minWave = 2;
        m_maxWave = 3;

        m_name = "Hellhound";
        m_desc = $"Gets +1 power for each other Hellhound within {m_effectRange} range.";

        AddKeyword(new GameImmuneToLavaKeyword(), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameVictoriousKeyword(new GameGainStaminaRangeAction(this, 1, 2)), false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int GetPower()
    {
        int basePower = base.GetPower();

        if (GetGameTile() != null)
        {
            List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_effectRange);

            for (int i = 0; i < surroundingTiles.Count; i++)
            {
                if (surroundingTiles[i].IsOccupied() && !surroundingTiles[i].m_occupyingUnit.m_isDead &&
                    surroundingTiles[i].m_occupyingUnit is ContentHellhoundEnemy)
                {
                    basePower += m_effectIncrease;
                }
            }
        }

        return basePower;
    }
}