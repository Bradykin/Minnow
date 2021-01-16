using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHellhoundEnemy : GameEnemyUnit
{
    int m_effectIncrease = 5;
    
    public ContentHellhoundEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 14;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_attack = 3;
        m_attackSFX = AudioHelper.RaptorAttack;

        m_aoeRange = 4;
        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Hellhound";
        m_desc = $"Gets +{m_effectIncrease}/+0 for each other Hellhound within {m_aoeRange} range.\n";

        AddKeyword(new GameLavawalkKeyword(), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameVictoriousKeyword(new GameGainStaminaRangeAction(this, 1, 2)), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int GetAttack()
    {
        int baseAttack = base.GetAttack();

        if (GetGameTile() != null)
        {
            List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_aoeRange);

            for (int i = 0; i < surroundingTiles.Count; i++)
            {
                if (surroundingTiles[i].IsOccupied() && !surroundingTiles[i].GetOccupyingUnit().m_isDead &&
                    surroundingTiles[i].GetOccupyingUnit() is ContentHellhoundEnemy)
                {
                    baseAttack += m_effectIncrease;
                }
            }
        }

        return baseAttack;
    }
}