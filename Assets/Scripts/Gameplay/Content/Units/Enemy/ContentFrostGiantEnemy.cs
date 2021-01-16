using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentFrostGiantEnemy : GameEnemyUnit
{
    private int m_stormRadius = 2;

    public ContentFrostGiantEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 60;
        m_maxStamina = 4;
        m_staminaRegen = 2;
        m_attack = 18;
        m_attackSFX = AudioHelper.SlamHeavy;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Frost Giant";

        m_desc = $"Emits a powerful storm around itself at range {m_stormRadius}. Whenever a player unit moves inside the storm, they take {Constants.WinterStormDamage} damage. While inside the storm, their vision range is reduce to {Constants.WinterStormVisionRange}.\n";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_desc += "If this is killed by another unit, that unit loses 1 stamina regen.";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_stormRadius, 0);
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            surroundingTiles[i].m_numCauseStorm++;
        }

        StormFogUpdate();
    }

    public override void OnMoveBegin()
    {
        base.OnMoveBegin();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_stormRadius, 0);
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            surroundingTiles[i].m_numCauseStorm--;
        }
    }

    public override void OnMoveEnd()
    {
        base.OnMoveEnd();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_stormRadius, 0);
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            surroundingTiles[i].m_numCauseStorm++;
        }

        StormFogUpdate();
    }

    public override int GetHitByUnit(int damage, GameUnit gameUnit, bool canReturnThorns)
    {
        int toReturn = base.GetHitByUnit(damage, gameUnit, canReturnThorns);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            if (m_isDead && !gameUnit.m_isDead)
            {
                gameUnit.RemoveMaxStamina(1, false);
            }
        }

        return toReturn;
    }

    private void StormFogUpdate()
    {
        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_stormRadius, 0);
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            List<GameTile> neighbourTiles = WorldGridManager.Instance.GetSurroundingGameTiles(surroundingTiles[i], 1, 0);
            bool keepRevealed = neighbourTiles.Any(t => (t.IsOccupied() && t.GetOccupyingUnit().GetTeam() == Team.Player) ||
                                                        (t.HasBuilding() && t.GetBuilding().GetTeam() == Team.Player) ||
                                                        !t.IsStorm());
            if (!keepRevealed)
            {
                surroundingTiles[i].m_isFog = true;
                surroundingTiles[i].m_isSoftFog = true;
            }
        }
    }
}