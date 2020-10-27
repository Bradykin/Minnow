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

        if (player == null)
        {
            return;
        }

        GameBuildingBase toPlace = GameBuildingFactory.GetBuildingClone(building);

        targetTile.PlaceBuilding(toPlace);
        player.AddControlledBuilding(toPlace);

        UIHelper.ClearDefensiveBuildingTiles();
        UIHelper.SetDefensiveBuildingTiles();
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

    public static int RelicCount<T>()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return 0;
        }

        return player.GetRelics().GetNumRelics<T>();
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
        return WorldController.Instance.m_gameController.m_currentTurn == WorldController.Instance.m_gameController.m_player;
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

    public static void ReturnToLevelSelectFromLevelScene(bool givePlaythroughRewards)
    {
        FactoryManager.Instance.StopAllCoroutines();
        Globals.m_levelActive = false;

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

        return WorldController.Instance.m_gameController.m_currentTurn == GameHelper.GetOpponent();
    }

    public static bool IsInGame()
    {
        return WorldController.Instance.m_isInGame;
    }
}
