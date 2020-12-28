using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentVolcanoGolemEnemy : GameEnemyUnit
{
    public ContentVolcanoGolemEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 40;
        m_maxStamina = 3;
        m_staminaRegen = 2;
        m_power = 15;
        m_attackSFX = AudioHelper.SlamHeavy;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;
        m_aoeRange = 1;

        m_name = "Volcano Golem";

        AddKeyword(new GameRangeKeyword(2), true, false);
        AddKeyword(new GameLavawalkKeyword(), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_aoeRange = 3;
        }

        m_desc = $"When this unit dies, all plains and forest tiles within {m_aoeRange} range are turned to lava.\n";

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_aoeRange, 0);
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            if (!surroundingTiles[i].HasBuilding() && (surroundingTiles[i].GetTerrain().IsPlains() || surroundingTiles[i].GetTerrain().IsForest()))
            {
                surroundingTiles[i].SetTerrain(new ContentLavaFieldActiveTerrain());
            }
        }

        base.Die(canRevive, damageType);
    }
}