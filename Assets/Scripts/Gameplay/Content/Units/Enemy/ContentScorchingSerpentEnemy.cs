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
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 4;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Scorching Serpent";
        m_desc = "When this unit attacks another, it knocks that unit 1 tile away from itself.\n";

        AddKeyword(new GameFlyingKeyword(), false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_desc = "Only takes 1 stamina to attack. When this unit attacks another, it knocks that unit 1 tile away from itself.\n";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int GetStaminaToAttack()
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            return 1;
        }
        else
        {
            return base.GetStaminaToAttack();
        }
    }

    public override int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool shouldThorns = true)
    {
        int amount = base.HitUnit(other, damageAmount, spendStamina, shouldThorns);

        if (other.m_isDead)
        {
            return amount;
        }

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), 1, 0);
        List<GameTile> surroundingTilesTarget = WorldGridManager.Instance.GetSurroundingGameTiles(other.GetGameTile(), 1).Where(t => !surroundingTiles.Contains(t) && !t.IsOccupied() && t.IsPassable(other, false)).ToList();

        if (surroundingTilesTarget.Count > 1)
        {
            other.m_worldUnit.MoveTo(surroundingTilesTarget[Random.Range(0, surroundingTilesTarget.Count)], false);
        }

        return amount;
    }
}