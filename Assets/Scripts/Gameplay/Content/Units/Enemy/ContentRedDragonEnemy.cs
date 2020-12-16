using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRedDragonEnemy : GameEnemyUnit
{
    public ContentRedDragonEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 4;
        m_maxStamina = 4;
        m_staminaRegen = 2;
        m_power = 3;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Red Dragon";
        
        m_desc = "If this unit attacks a unit in a forest, it burns the forest and deals double damage.";

        AddKeyword(new GameFlyingKeyword(), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            AddKeyword(new GameDamageReductionKeyword(2), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int HitUnit(GameUnit other, int damageAmount, bool spendStamina = true, bool isThornsAttack = false, bool canCleave = true)
    {
        if (other.GetGameTile().GetTerrain().IsForest() && other.GetGameTile().GetTerrain().CanBurn())
        {
            damageAmount *= 2;
            other.GetGameTile().SetTerrain(GameTerrainFactory.GetBurnedTerrainClone(other.GetGameTile().GetTerrain()));
        }
        
        return base.HitUnit(other, damageAmount, spendStamina, isThornsAttack, canCleave);
    }

    public override int HitBuilding(GameBuildingBase other, bool spendStamina = true, bool canCleave = true)
    {
        if (other.GetGameTile().GetTerrain().IsForest() && other.GetGameTile().GetTerrain().CanBurn())
        {
            other.GetGameTile().SetTerrain(GameTerrainFactory.GetBurnedTerrainClone(other.GetGameTile().GetTerrain()));
        }

        return base.HitBuilding(other, spendStamina, canCleave);
    }
}