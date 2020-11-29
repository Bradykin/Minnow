using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFrostGiantEnemy : GameEnemyUnit
{
    private int m_stormRadius = 2;
    private int m_stormDamage = 3;
    private int m_sightReductionAmount = 2;

    public ContentFrostGiantEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 60;
        m_maxStamina = 4;
        m_staminaRegen = 2;
        m_power = 18;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Frost Giant";

        m_desc = $"Emits a powerful storm around itself at range {m_stormRadius}. Whenever a player unit moves inside the storm, they take {m_stormDamage} damage. While inside the storm, they have {m_sightReductionAmount  } reduced sight range.\n";

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
    }

    public override void OnOtherMove(GameUnit other, GameTile startingTile, GameTile endingTile, List<GameTile> pathBetweenTiles)
    {
        base.OnOtherMove(other, startingTile, endingTile, pathBetweenTiles);

        if (other.m_isDead)
        {
            return;
        }

        int distanceBetweenTiles = WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(GetGameTile(), endingTile);

        if (distanceBetweenTiles <= m_stormRadius)
        {
            other.GetHitByAbility(m_stormDamage);
        }
    }

    public override int GetHitByUnit(int damage, GameUnit gameUnit)
    {
        int toReturn = base.GetHitByUnit(damage, gameUnit);

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            if (m_isDead && !gameUnit.m_isDead)
            {
                gameUnit.RemoveMaxStamina(1, false);
            }
        }

        return toReturn;
    }
}