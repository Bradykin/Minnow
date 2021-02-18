using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentSkeletalPirateEnemy : GameEnemyUnit
{
    private int m_deathRange = 2;
    
    public ContentSkeletalPirateEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 2 + GetHealthModByWave();
        m_maxStamina = 4;
        m_staminaRegen = 3;
        m_attack = 0 + GetAttackModByWave();
        m_attackSFX = AudioHelper.SwordLight;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Skeletal Pirate";
        m_desc = "This unit is part of the pirate crew.\n";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_desc += $"So long as there is any Skeletal Captains or Zombie Crabs within range {m_deathRange}, this unit cannot die"; 
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }
    private int GetHealthModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        int scalingValue = waveNum;
        if (waveNum >= 3)
        {
            scalingValue += (waveNum - 2);
        }

        return scalingValue * 6;
    }

    private int GetAttackModByWave()
    {
        int waveNum = GameHelper.GetCurrentWaveNum();

        return waveNum * 3;
    }

    protected override bool ShouldRevive(out int healthSurviveAt)
    {
        bool shouldRevive = base.ShouldRevive(out int surviveAt);
        healthSurviveAt = surviveAt;

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_deathRange);

        if (surroundingTiles.Any(t => t.IsOccupied() && (t.GetOccupyingUnit() is ContentSkeletalCaptainEnemy || t.GetOccupyingUnit() is ContentZombieCrabEnemy)))
        {
            shouldRevive = true;
        }

        return shouldRevive;
    }
}