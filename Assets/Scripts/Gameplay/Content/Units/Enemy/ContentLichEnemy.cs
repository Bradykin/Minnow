using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLichEnemy : GameEnemyUnit
{
    public bool m_hasReanimated = false;

    public ContentLichEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_maxHealth = 600;
            m_maxStamina = 7;
            m_staminaRegen = 7;
            m_attack = 30;
        }
        else
        {
            m_maxHealth = 300;
            m_maxStamina = 5;
            m_staminaRegen = 5;
            m_attack = 15;
        }

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isBoss = true;
        m_aoeRange = 3;
        m_attackSFX = AudioHelper.SpellAttackMedium;

        m_name = "Lich";
        m_desc = $"The final boss. Kill it, and win.\nAll healing done to player units within range {m_aoeRange} is instead converted into damage.\nAny player units that die within range {m_aoeRange} are reanimated as a <b>Husk</b> that gains their stats and <b>keywords</b>.\n";

        AddKeyword(new GameRangeKeyword(2), true, false);
        AddKeyword(new GameFlyingKeyword(), true, false);

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

        GetWorldTile().ClearSurroundingFog(2);

        if (!m_hasReanimated)
        {
            UIHelper.CreateHUDNotification("Boss Arrived", "The Lich has arrived and brought death to the world!");
        }
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        base.Die(canRevive, damageType);

        GameHelper.GetGameController().m_activeBossUnits.Remove(this);

        if (m_hasReanimated)
        {
            GameHelper.EndLevel(RunEndType.Win);
        }
        else
        {
            List<WorldTile> validTiles = new List<WorldTile>();
            List<WorldTile> tilesInRange = WorldGridManager.Instance.GetSurroundingWorldTiles(GameHelper.GetPlayer().GetCastleWorldTile(), Constants.GridSizeX, 12);

            ContentLichCastleBuilding lichCastle = new ContentLichCastleBuilding();

            for (int i = 0; i < tilesInRange.Count; i++)
            {
                if (!tilesInRange[i].GetGameTile().IsOccupied() && !tilesInRange[i].GetGameTile().HasBuilding() && lichCastle.IsValidTerrainToPlace(tilesInRange[i].GetGameTile().GetTerrain(), tilesInRange[i].GetGameTile()))
                {
                    validTiles.Add(tilesInRange[i]);
                }
            }

            int r = UnityEngine.Random.Range(0, validTiles.Count);


            List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(validTiles[r].GetGameTile(), 3, 0);
            for (int i = 0; i < surroundingTiles.Count; i++)
            {
                if (surroundingTiles[i].GetTerrain().IsMountain() || surroundingTiles[i].GetTerrain().IsWater())
                {
                    continue;
                }
                
                float random = Random.Range(0.0f, 1.0f);
                if (random >= 0.33f)
                {
                    surroundingTiles[i].SetTerrain(new ContentAshPlainsTerrain(), true);
                }
                else
                {
                    surroundingTiles[i].SetTerrain(new ContentAshForestBurnedTerrain(), true);
                }
            }

            validTiles[r].GetGameTile().PlaceBuilding(lichCastle);
            validTiles[r].ClearSurroundingFog(3);
            UIHelper.CreateHUDNotification("Lich Escaped", "The Lich has retreated to his ancient necropolis that stores his phylactery. You'll need to hunt him down and destroy the phylactery to defeat him once and for all!");
        }
    }
}