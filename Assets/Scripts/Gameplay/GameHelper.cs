using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameHelper
{
    //Returns true if it hits the chance, false if it does not
    public static bool PercentChanceRoll(int percent)
    {
        return (Random.Range(1, 101) <= percent);
    }

    public static void MakePlayerUnit(GameTile targetTile, GameUnit unit)
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        targetTile.PlaceUnit(unit);

        player.AddControlledUnit(unit);
    }

    public static void MakePlayerBuilding(GameTile targetTile, GameBuildingBase building)
    {
        GamePlayer player = GameHelper.GetPlayer();
        
        GameBuildingBase toPlace = GameBuildingFactory.GetBuildingClone(building);

        targetTile.PlaceBuilding(toPlace);
        player.AddControlledBuilding(toPlace);

        UIHelper.ClearDefensiveBuildingTiles();
        UIHelper.SetDefensiveBuildingTiles();
    }

    public static void DestroyPlayerBuilding(GameTile buildingTile)
    {
        GamePlayer player = GameHelper.GetPlayer();

        player.RemoveControlledBuilding(buildingTile.GetBuilding());
        buildingTile.ClearBuilding();
        if (buildingTile.GetTerrain().IsMountain() == false && buildingTile.GetTerrain().IsWater() == false)
        {
            buildingTile.SetTerrain(new ContentRubbleTerrain(), true);
        }
    }

    public static GamePlayer GetPlayer()
    {
        if (WorldController.Instance == null)
        {
            return null;
        }

        if (WorldController.Instance.m_gameController == null)
        {
            return null;
        }

        return WorldController.Instance.m_gameController.m_player;
    }

    public static GameOpponent GetOpponent()
    {
        if (WorldController.Instance == null)
        {
            return null;
        }

        if (WorldController.Instance.m_gameController == null)
        {
            return null;
        }

        return WorldController.Instance.m_gameController.m_gameOpponent;
    }

    public static GameController GetGameController()
    {
        if (WorldController.Instance == null)
        {
            return null;
        }

        if (WorldController.Instance.m_gameController == null)
        {
            return null;
        }

        return WorldController.Instance.m_gameController;
    }

    public static int GetCurrentWaveNum()
    {
        if (WorldController.Instance == null)
        {
            return 1;
        }

        if (WorldController.Instance.m_gameController == null)
        {
            return 1;
        }

        return WorldController.Instance.m_gameController.m_currentWaveNumber;
    }

    public static bool HasRelic<T>()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return false;
        }

        if (player.GetRelics().GetNumRelics<T>() > 0)
        {
            return true;
        }

        return false;
    }

    public static bool IsValidChaosLevel(Globals.ChaosLevels toCheck)
    {
        if (WorldController.Instance.m_gameController == null)
        {
            return false;
        }

        if (WorldController.Instance.m_gameController.m_map.m_difficulty == MapDifficulty.Introduction)
        {
            return false;
        }

        if (Globals.m_curChaos >= (int)toCheck)
        {
            return true;
        }

        return false;
    }

    public static bool IsPlayerTurn()
    {
        return WorldController.Instance.m_gameController.CurrentActor == WorldController.Instance.m_gameController.m_player;
    }

    public static List<GameCard> GetPlayerBaseDeckOfUnits()
    {
        List<GameCard> deckOfUnits = new List<GameCard>();

        foreach (var card in GetPlayer().m_deckBase.GetCardsForRead())
        {
            if (card is GameUnitCard)
            {
                deckOfUnits.Add(card);
            }
        }

        return deckOfUnits;
    }

    public static List<GameCard> GetPlayerBaseDeckOfSpells()
    {
        List<GameCard> deckOfSpells = new List<GameCard>();

        foreach (var card in GetPlayer().m_deckBase.GetCardsForRead())
        {
            if (card is GameCardSpellBase)
            {
                deckOfSpells.Add(card);
            }
        }

        return deckOfSpells;
    }

    public static void ReturnToLevelSelectFromLevelScene()
    {
        FactoryManager.Instance.StopAllCoroutines();

        WorldController.Instance.EndLevel(RunEndType.Loss);
        WorldGridManager.Instance.RecycleGrid();

        SceneLoader.ActivateScene("LevelSelectScene", "LevelScene");
    }

    public static bool IsInLevelSelect()
    {
        return SceneLoader.CurrentScene == "LevelSelectScene";
    }

    public static bool IsInLevelBuilder()
    {
        return SceneLoader.CurrentScene == "LevelCreatorScene";
    }

    public static bool IsBossOrElite(GameUnit toCheck)
    {
        if (toCheck is GameEnemyUnit)
        {
            if (((GameEnemyUnit)toCheck).m_isElite || ((GameEnemyUnit)toCheck).m_isBoss)
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsOpponentsTurn()
    {
        if (!IsInGame())
        {
            return false;
        }

        return WorldController.Instance.m_gameController.CurrentActor == GameHelper.GetOpponent();
    }

    public static bool IsInGame()
    {
        return WorldController.Instance.m_isInGame;
    }

    public static bool HasAllTypelines()
    {
        Dictionary<int, int> numCreatureTypes = new Dictionary<int, int>();
        List<GameUnit> gameUnits = GameHelper.GetPlayer().m_controlledUnits;
        for (int i = 0; i < gameUnits.Count; i++)
        {
            int typelineInt = (int)gameUnits[i].GetTypeline();
            if (!numCreatureTypes.ContainsKey(typelineInt))
            {
                numCreatureTypes.Add(typelineInt, 1);
            }
            else
            {
                numCreatureTypes[typelineInt]++;
            }
        }

        bool hasAll = true;
        for (int i = 0; i < (int)Typeline.Count; i++)
        {
            if (!numCreatureTypes.ContainsKey(0))
            {
                hasAll = false;
                break;
            }
        }

        return hasAll;
    }

    public static bool IsUnitInWorld(GameUnit toTest)
    {
        if (toTest.GetGameTile() == null)
        {
            return false;
        }

        if (Globals.m_inDeckView)
        {
            return false;
        }

        return true;
    }
}
