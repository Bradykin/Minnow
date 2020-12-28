using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGreenRockGiantEnemy : GameEnemyUnit
{
    private int m_regenerateAmount = 10;
    
    public ContentGreenRockGiantEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 65;
        m_maxStamina = 4;
        m_staminaRegen = 3;
        m_power = 13;
        m_attackSFX = AudioHelper.MetalClangAttack;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Common;

        m_name = "Green Rock Giant";
        
        m_desc = "This unit gains <b>Brittle</b> when not on a Mountain or Hills.\n";

        AddKeyword(new GameMountainwalkKeyword(), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_desc += $"This gains <b>Regen</b> {m_regenerateAmount} while on a Mountain or Hills.\n";
        }

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override GameRegenerateKeyword GetRegenerateKeyword()
    {
        GameRegenerateKeyword regenKeyword = base.GetRegenerateKeyword();

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            if (GetGameTile().GetTerrain().IsHill() || GetGameTile().GetTerrain().IsMountain())
            {
                if (regenKeyword == null)
                {
                    regenKeyword = new GameRegenerateKeyword(m_regenerateAmount);
                }
                else
                {
                    regenKeyword.AddKeyword(new GameRegenerateKeyword(m_regenerateAmount));
                }
            }
        }

        return regenKeyword;
    }

    public override GameBrittleKeyword GetBrittleKeyword()
    {
        if (!GetGameTile().GetTerrain().IsHill() && !GetGameTile().GetTerrain().IsMountain())
        {
            return new GameBrittleKeyword();
        }
        
        return base.GetBrittleKeyword();
    }
}