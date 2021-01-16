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

        UIHelper.ClearBuildingTiles();
        UIHelper.SetBuildingTiles();

        GameNotificationManager.RecordBuilding(building);
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

    public static T GetBoss<T>() where T : GameEnemyUnit
    {
        if (!GameHelper.IsInGame())
        {
            return null;
        }

        if (GameHelper.GetGameController() == null)
        {
            return null;
        }

        List<GameEnemyUnit> activeBossUnits = GameHelper.GetGameController().m_activeBossUnits;
        for (int i = 0; i < activeBossUnits.Count; i++)
        {
            if (activeBossUnits[i] is T matchingBoss)
            {
                return matchingBoss;
            }
        }

        return null;
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

    public static void EndLevel(RunEndType runEndType)
    {
        UIWinLossController.Instance.Init(runEndType);

        FactoryManager.Instance.StopAllCoroutines();

        WorldController.Instance.EndLevel(runEndType);

        WorldGridManager.Instance.RecycleGrid();
        UIRelicController.Instance.ClearRelics();
    }

    public static void ReturnToLevelSelectFromLevelScene()
    {
        SceneLoader.ActivateScene("LevelSelectScene", "LevelScene");

        UILevelSelectController.Instance.SetSelectedLevel(null);
        UICameraController.Instance.transform.position = UICameraController.Instance.m_levelSelectReturnTransform;

        AudioBackgroundController.Instance.StopBackgroundMusic();
    }

    public static bool IsInLevelSelect()
    {
        return SceneLoader.CurrentScene == "LevelSelectScene";
    }

    public static bool IsInLevelBuilder()
    {
        return SceneLoader.CurrentScene == "LevelCreatorScene";
    }

    public static bool IsInWinLoss()
    {
        return UIWinLossController.Instance.IsActive();
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

    public static bool CardInPlayerDeck(GameCard card)
    {
        for (int i = 0; i < GetPlayer().m_deckBase.Count(); i++)
        {
            if (card == GetPlayer().m_deckBase.GetCardByIndex(i))
            {
                return true;
            }
        }

        return false;
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
            if (!numCreatureTypes.ContainsKey(i))
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

    public static GameElementBase.GameRarity SelectIntermissionUnitRarity()
    {
        GameController gameController = GameHelper.GetGameController();

        GameElementBase.GameRarity gameRarity;

        if ((gameController.m_currentWaveNumber == 2) ||
            (gameController.m_previousRareUnitOptionWave == 3 && gameController.m_currentWaveNumber == 4) ||
            (gameController.m_numRareUnitOptionsOffered == 2))
        {
            gameRarity = GameCardFactory.GetRandomRarity();
            while (gameRarity == GameElementBase.GameRarity.Rare)
            {
                gameRarity = GameCardFactory.GetRandomRarity();
            }
        }
        else if (gameController.m_numRareUnitOptionsOffered == 0 && gameController.m_currentWaveNumber == 6)
        {
            gameRarity = GameElementBase.GameRarity.Rare;
        }
        else
        {
            gameRarity = GameCardFactory.GetRandomRarity();
        }

        if (gameRarity == GameElementBase.GameRarity.Rare)
        {
            gameController.m_numRareUnitOptionsOffered++;
            gameController.m_previousRareUnitOptionWave = gameController.m_currentWaveNumber;
        }

        return gameRarity;
    }

    public static bool IsCurrentMapLakeside()
    {
        return GetGameController().GetCurMap().m_id == 0;
    }

    public static void PlayCardOnUnit(UICard card, GameUnit target)
    {
        WorldController.Instance.PlayCard(card);
        card.m_card.PlayCard(target);
        WorldController.Instance.PostPlayCard();
    }
}
