using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentChillflameBeastEnemy : GameEnemyUnit
{
    private int m_deathMeltRange = 1;
    
    public ContentChillflameBeastEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 40;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_attack = 14;
        m_attackSFX = AudioHelper.SlamHeavy;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Uncommon;

        m_name = "Chillflame Beast";

        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            m_deathMeltRange = 2;
        }
        m_desc = "When this unit dies, it overheats and melts all ice tiles in range {m_deathMeltRange}\n";

        AddKeyword(new GameFrostwalkKeyword(), true, false);

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        GameTile gameTile = GetGameTile();
        
        base.Die(canRevive, damageType);

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(gameTile, m_deathMeltRange, 0);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            if (surroundingTiles[i].GetTerrain().IsIce())
            {
                surroundingTiles[i].SetTerrain(new ContentIceWaterTerrain(), true);
            }
        }
    }
}