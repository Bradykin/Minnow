using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentIcefisherEnemy : GameEnemyUnit
{
    private int m_statIncrease = 10;
    private int m_damageReductionAmount = 4;
    
    public ContentIcefisherEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 4;
        m_maxStamina = 4;
        m_staminaRegen = 4;
        m_power = 2;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Icefisher";
        m_desc = "Can use its full turn to break a nearby cracked ice tile.\n";

        AddKeyword(new GameWaterwalkKeyword(), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_desc += $"Gets +{m_statIncrease}/+0 and Damage Reduction {m_damageReductionAmount} while in or adjacent to a water tile.";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIIcefisherIcebreakStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override int GetPower()
    {
        int toReturn = base.GetPower();

        if (!GameHelper.IsInGame() || GetGameTile() == null)
        {
            return toReturn;
        }

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), 1, 0);
            if (surroundingTiles.Any(t => t.GetTerrain().IsWater()))
            {
                toReturn += 10;
            }
        }

        return toReturn;
    }

    public override GameDamageReductionKeyword GetDamageReductionKeyword()
    {
        GameDamageReductionKeyword toReturn = base.GetDamageReductionKeyword();

        if (GameHelper.IsUnitInWorld(this))
        {
            List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), 1, 0);
            if (surroundingTiles.Any(t => t.GetTerrain().IsWater()))
            {
                if (toReturn == null)
                {
                    toReturn = new GameDamageReductionKeyword(m_damageReductionAmount);
                }
                else
                {
                    toReturn.AddKeyword(new GameDamageReductionKeyword(m_damageReductionAmount));
                }
            }
        }

        return toReturn;
    }
}