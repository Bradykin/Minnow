using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentIceWurmEnemy : GameEnemyUnit
{
    public ContentIceWurmEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 28;
        m_maxStamina = 4;
        m_staminaRegen = 4;
        m_power = 9;
        m_attackSFX = AudioHelper.SlamHeavy;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;
        m_alwaysIgnoreDifficultTerrain = true;

        m_name = "Ice Wurm";
        
        m_desc = "Ignores difficult terrain.\n";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_maxStamina += 1;
            m_staminaRegen += 1;
            m_desc += "After moving, root all player units in range 1 until the end of their turn.\n";
            m_aoeRange = 1;
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void OnMoveEnd()
    {
        base.OnMoveEnd();

        List<GameTile> gameTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_aoeRange);

        for (int i = 0; i < gameTiles.Count; i++)
        {
            if (gameTiles[i].IsOccupied() && gameTiles[i].GetOccupyingUnit().GetTeam() != GetTeam() && gameTiles[i].GetOccupyingUnit().GetRootedKeyword() == null)
            {
                gameTiles[i].GetOccupyingUnit().AddKeyword(new GameRootedKeyword(), false, false);
                GameHelper.GetPlayer().AddScheduledAction(ScheduledActionTime.EndOfTurn, new GameLoseKeywordAction(gameTiles[i].GetOccupyingUnit(), new GameRootedKeyword()));
            }
        }
    }
}