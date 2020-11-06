using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentVolcanoGolemEnemy : GameEnemyUnit
{
    int m_effectRange = 1;
    
    public ContentVolcanoGolemEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 40;
        m_maxStamina = 3;
        m_staminaRegen = 2;
        m_power = 30;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_minWave = 4;
        m_maxWave = 6;

        m_name = "Volcano Golem";

        AddKeyword(new GameRangeKeyword(2), false);
        AddKeyword(new GameLavawalkKeyword(), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_effectRange = 3;
        }

        m_desc = $"When this unit dies, all plains and forest tiles within {m_effectRange} range are turned to lava.";

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void Die(bool canRevive = true)
    {
        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_effectRange, 0);
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            if (!surroundingTiles[i].HasBuilding() && (surroundingTiles[i].GetTerrain().IsPlains() || surroundingTiles[i].GetTerrain().IsForest()))
            {
                surroundingTiles[i].SetTerrain(new ContentLavaFieldActiveTerrain());
            }
        }

        base.Die(canRevive);
    }
}