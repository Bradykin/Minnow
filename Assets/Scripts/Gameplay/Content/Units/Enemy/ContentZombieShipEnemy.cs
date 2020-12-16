using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContentZombieShipEnemy : GameEnemyUnit
{
    private int m_auraRange = 3;
    private int m_damageOrHealingAmount = 8;
    public bool m_isEliteShip;
    public bool m_hasReleasedUnits;

    public ContentZombieShipEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 200;
        m_maxStamina = 3;
        m_staminaRegen = 3;
        m_power = 6;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;

        m_name = "Zombie Ship";
        
        m_desc = $"At the end of each turn, this unit will radiate necromantic energy, healing all nearby members of its crew and damaging all player units in range {m_auraRange} for {m_damageOrHealingAmount}./n";


        AddKeyword(new GameWaterboundKeyword(), true, false);
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.AddEnemyAbility))
        {
            //AddKeyword(new GameFlyingKeyword(), true, false);
        }

        m_AIGameEnemyUnit.AddAIStep(new AIZombieShipSeekLandStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        GetWorldTile().ClearSurroundingFog(2);
    }

    public override void OnMoveEnd()
    {
        base.OnMoveEnd();

        GetWorldTile().ClearSurroundingFog(2);
    }

    public override void EndTurn()
    {
        base.EndTurn();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_auraRange, 0);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            if (!surroundingTiles[i].IsOccupied())
            {
                continue;
            }

            GameUnit unit = surroundingTiles[i].GetOccupyingUnit();
            if (unit is ContentSkeletalPirateEnemy || unit is ContentSkeletalCaptainEnemy || unit is ContentZombieCrabEnemy)
            {
                unit.Heal(m_damageOrHealingAmount);
            }
            else if (unit.GetTeam() == Team.Player)
            {
                unit.GetHitByAbility(m_damageOrHealingAmount);
            }
        }
    }
    
    public bool TryReleaseUnits()
    {
        int numFreeSpacesNeeded = 3;
        if (m_isEliteShip)
        {
            numFreeSpacesNeeded++;
        }

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), 1);
        if (!surroundingTiles.Any(t => !t.GetTerrain().IsWater()))
        {
            return false;
        }

        List<GameTile> surroundingPassableTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), 2).Where(t => !t.IsOccupied() && t.IsPassable(null, false)).ToList();

        if (surroundingPassableTiles.Count < numFreeSpacesNeeded)
        {
            return false;
        }

        surroundingPassableTiles.OrderBy(t => WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(GetGameTile(), t));


        for (int i = 0; i < numFreeSpacesNeeded; i++)
        {
            GameEnemyUnit newEnemyUnit;
            if (i == 0)
            {
                newEnemyUnit = GameUnitFactory.GetEnemyUnitClone(new ContentSkeletalCaptainEnemy(WorldController.Instance.m_gameController.m_gameOpponent));
            }
            else if (i == 1 && m_isEliteShip)
            {
                newEnemyUnit = GameUnitFactory.GetEnemyUnitClone(new ContentZombieCrabEnemy(WorldController.Instance.m_gameController.m_gameOpponent));
            }
            else
            {
                newEnemyUnit = GameUnitFactory.GetEnemyUnitClone(new ContentSkeletalPirateEnemy(WorldController.Instance.m_gameController.m_gameOpponent));
            }
            surroundingPassableTiles[i].PlaceUnit(newEnemyUnit);
            newEnemyUnit.OnSummon();
            WorldController.Instance.m_gameController.m_gameOpponent.AddControlledUnit(newEnemyUnit);
        }

        UIHelper.CreateWorldElementNotification("The ghostly ship has disembarked its crew!", false, m_worldUnit.gameObject);
        m_hasReleasedUnits = true;

        return true;
    }

    public override JsonGameUnitData SaveToJson()
    {
        JsonGameUnitData jsonData = base.SaveToJson();

        jsonData.boolValue1 = m_isEliteShip;
        jsonData.boolValue2 = m_hasReleasedUnits;

        return jsonData;
    }

    public override void LoadFromJson(JsonGameUnitData jsonData)
    {
        base.LoadFromJson(jsonData);

        m_isEliteShip = jsonData.boolValue1;
        m_hasReleasedUnits = jsonData.boolValue2;
    }
}