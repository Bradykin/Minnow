using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Target buildings over units
public class ContentScorchingSerpentEnemy : GameEnemyUnit
{
    public ContentScorchingSerpentEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 7;
        m_maxStamina = 6;
        m_staminaRegen = 4;
        m_power = 4;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Scorching Serpent";
        m_desc = "";

        m_minWave = 1;
        m_maxWave = 2;

        AddKeyword(new GameFlyingKeyword(), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_desc = "When this unit attacks another, it knocks that unit 1 tile away from itself.";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackOnceStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool shouldThorns = true)
    {
        int amount = base.HitUnit(other, damageAmount, spendStamina, shouldThorns);

        if (other.m_isDead)
        {
            return amount;
        }

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), 0);
        List<GameTile> surroundingTilesTarget = WorldGridManager.Instance.GetSurroundingGameTiles(other.GetGameTile(), 1).Where(t => !surroundingTiles.Contains(t) && !t.IsOccupied() && t.IsPassable(other, false)).ToList();

        if (surroundingTilesTarget.Count > 1)
        {
            other.m_worldUnit.MoveTo(surroundingTilesTarget[Random.Range(0, surroundingTilesTarget.Count)], false);
        }

        return amount;
    }
}