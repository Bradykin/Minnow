using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentLordOfWinterEnemy : GameEnemyUnit
{
    private int m_stormRadius = 5;

    public ContentLordOfWinterEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_maxHealth = 900;
            m_maxStamina = 9;
            m_staminaRegen = 9;
            m_attack = 40;
        }
        else
        {
            m_maxHealth = 600;
            m_maxStamina = 8;
            m_staminaRegen = 8;
            m_attack = 20;
        }
        m_staminaToAttack = 4;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isBoss = true;
        m_attackSFX = AudioHelper.SlamHeavy;

        m_name = "Lord of Winter";
        m_desc = $"The final boss. Kill it, and win.\nTakes {m_staminaToAttack} Stamina to attack.\nEmits a powerful storm around itself at range {m_stormRadius}. Whenever a player unit moves inside the storm, they take {Constants.WinterStormDamage} damage. While inside the storm, their vision range is reduce to {Constants.WinterStormVisionRange}.\nWhen this unit attacks, the target loses 1 max Stamina.\n";

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        GameHelper.GetGameController().m_activeBossUnits.Add(this);

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_stormRadius, 0);
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            surroundingTiles[i].m_numCauseStorm++;
        }

        StormFogUpdate();

        UIHelper.CreateHUDNotification("Boss Arrived", "The Lord of Winter has arrived and summoned the endless winter!");
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

    public override int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool shouldThorns = true, bool canCleave = true)
    {
        int toReturn = base.HitUnit(other, damageAmount, spendStamina, shouldThorns, canCleave);

        if (!other.m_isDead)
        {
            other.RemoveMaxStamina(1, false);
        }

        return toReturn;
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        base.Die(canRevive, damageType);

        GameHelper.GetGameController().m_activeBossUnits.Remove(this);

        GameHelper.EndLevel(RunEndType.Win);
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