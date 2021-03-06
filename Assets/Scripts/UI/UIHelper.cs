﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using System.IO;
using UnityEngine.EventSystems;

public static class UIHelper
{
    public static Color m_defaultColor = new Color(Color.white.r, Color.white.g, Color.white.b, 1.0f);
    public static Color m_fadedColor = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);

    public static Color m_defaultTint = new Color(Color.white.r, Color.white.g, Color.white.b, 0f);
    public static Color m_stormTint = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 0.4f);
    public static Color m_defaultFaded = new Color(Color.white.r, Color.white.g, Color.white.b, 0.4f);
    public static Color m_selectedTint = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.3f);
    public static Color m_selectedHarshTint = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 1f);
    public static Color m_validTint = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, 0.3f);
    public static Color m_invalidTint = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
    public static Color m_attackTint = new Color(Color.green.r, Color.green.g, Color.green.b, 0.3f);
    public static Color m_spellcraftTint = new Color(128.0f, 0.0f, 128.0f, 0.3f);
    public static Color m_defensiveBuildingTint = new Color(Color.red.r, Color.red.g, Color.red.b, 0.2f);
    public static Color m_aoeTint = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 0.4f);

    public static Color m_valid = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 1.0f);
    public static Color m_validAltPlayer = new Color(Color.green.r, Color.green.g, Color.green.b, 1.0f);
    public static Color m_validAltEnemy = new Color(Color.red.r, Color.red.g, Color.red.b, 1.0f);
    public static Color m_invalid = new Color(Color.red.r, Color.red.g, Color.red.b, 1.0f);
    public static Color m_invalidAlt = new Color(Color.white.r, Color.white.g, Color.white.b, 1.0f);
    public static Color m_attackColor = new Color(Color.green.r, Color.green.g, Color.green.b, 1.0f);

    public static Color m_playerColorTint = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, 0.2f);
    public static Color m_playerColor = new Color(Color.cyan.r, Color.blue.g, Color.blue.b, 1f);
    public static Color m_enemyColorTint = new Color(Color.red.r, Color.red.g, Color.red.b, 0.2f);
    public static Color m_enemyColor = new Color(Color.red.r, Color.red.g, Color.red.b, 1f);

    public static Color m_difficultyNormal = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 1f);
    public static Color m_difficultyHard = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 1f);
    public static Color m_difficultyVeryHard = new Color(Color.red.r, Color.red.g, Color.red.b, 1f);

    public static Color m_commonRarityTint = new Color(Color.white.r, Color.white.g, Color.white.b, 0.1f);
    public static Color m_uncommonRarityTint = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, 0.7f);
    public static Color m_rareRarityTint = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 1.0f);
    public static Color m_noRarityTint = new Color(Color.white.r, Color.white.g, Color.white.b, 0.0f);

    public static Color m_commonRarity = new Color(Color.white.r, Color.white.g, Color.white.b, 1f);
    public static Color m_uncommonRarity = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, 1f);
    public static Color m_rareRarity = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 1f);
    public static Color m_noRarity = new Color(Color.white.r, Color.white.g, Color.white.b, 1.0f);

    public static Color GetRarityColorTint(GameElementBase.GameRarity rarity)
    {
        if (rarity == GameElementBase.GameRarity.Common)
        {
            return m_commonRarityTint;
        }

        if (rarity == GameElementBase.GameRarity.Uncommon)
        {
            return m_uncommonRarityTint;
        }

        if (rarity == GameElementBase.GameRarity.Rare)
        {
            return m_rareRarityTint;
        }

        return m_noRarityTint;
    }

    public static Sprite GetRelicRarityFrame(GameElementBase.GameRarity rarity)
    {
        if (rarity == GameElementBase.GameRarity.Common || rarity == GameElementBase.GameRarity.Starter)
        {
            return Resources.Load<Sprite>("UI2/Icons/CommonRelicRarityFrame45x45") as Sprite;
        }

        if (rarity == GameElementBase.GameRarity.Uncommon)
        {
            return Resources.Load<Sprite>("UI2/Icons/UncommonRelicRarityFrame45x45") as Sprite;
        }

        if (rarity == GameElementBase.GameRarity.Rare || rarity == GameElementBase.GameRarity.Special)
        {
            return Resources.Load<Sprite>("UI2/Icons/RareRelicRarityFrame45x45") as Sprite;
        }

        return null;
    }

    public static Dictionary<GameElementBase.GameRarity, Sprite> m_chestRarityDictionary = new Dictionary<GameElementBase.GameRarity, Sprite>();
    public static Dictionary<int, Sprite> m_iconGoldDictionary = new Dictionary<int, Sprite>();
    public static Dictionary<string, Sprite> m_altarDictionary = new Dictionary<string, Sprite>();
    public static Dictionary<string, Sprite> m_terrainDictionary = new Dictionary<string, Sprite>();
    public static Dictionary<string, Sprite> m_cardDictionary = new Dictionary<string, Sprite>();
    public static Dictionary<string, Sprite> m_unitDictionary = new Dictionary<string, Sprite>();
    public static Dictionary<string, Sprite> m_buildingDictionary = new Dictionary<string, Sprite>();
    public static Dictionary<string, Sprite> m_relicDictionary = new Dictionary<string, Sprite>();
    public static Dictionary<string, Sprite> m_mapDictionary = new Dictionary<string, Sprite>();
    public static Dictionary<string, Sprite> m_intermissionActionDictionary = new Dictionary<string, Sprite>();

    public static Sprite m_eventSprite = null;
    public static Sprite m_eventSpriteW = null;

    public static Sprite m_cardDefaultBackgroundSprite = null;
    public static Sprite m_cardGoldBackgroundSprite = null;

    public static Color GetRarityColor(GameElementBase.GameRarity rarity)
    {
        if (rarity == GameElementBase.GameRarity.Common)
        {
            return m_commonRarity;
        }

        if (rarity == GameElementBase.GameRarity.Uncommon)
        {
            return m_uncommonRarity;
        }

        if (rarity == GameElementBase.GameRarity.Rare)
        {
            return m_rareRarity;
        }

        return m_noRarity;
    }

    public static Color GetSelectTintColor(bool isSelected)
    {
        if (isSelected)
        {
            return m_selectedTint;
        }
        else
        {
            return GetDefaultTintColor();
        }
    }

    public static Color GetSelectHarshTintColor(bool isSelected)
    {
        if (isSelected)
        {
            return m_selectedHarshTint;
        }
        else
        {
            return GetDefaultTintColor();
        }
    }

    public static Color GetSelectValidTintColor(bool isValid)
    {
        if (isValid)
        {
            return m_selectedTint;
        }
        else
        {
            return GetValidTintColor(false);
        }
    }

    public static Color GetValidTintColor(bool isValid)
    {
        if (isValid)
        {
            return m_validTint;
        }
        else
        {
            return m_invalidTint;
        }
    }

    public static Color GetSpellcraftTint(int numSpellcraft)
    {
        Color returnColor = m_spellcraftTint;
        returnColor.a = returnColor.a + (0.4f * (numSpellcraft-1));

        return returnColor;
    }

    public static Color GetBuildingRangeTint(int numBuildings)
    {
        Color returnColor = m_defensiveBuildingTint;
        returnColor.a = returnColor.a + (0.2f * (numBuildings - 1));

        return returnColor;
    }

    public static Color GetAoeRangeTint(int count)
    {
        Color returnColor = m_aoeTint;
        //returnColor.a = returnColor.a + (0.2f * (count - 1)); nmartino - Try not using tint mulitplier for now

        return returnColor;
    }

    public static Color GetAttackTintColor()
    {
        return m_attackTint;
    }

    public static Color GetValidColor(bool isValid)
    {
        if (isValid)
        {
            return m_valid;
        }
        else
        {
            return m_invalid;
        }
    }

    public static Color GetValidColorAltByTeam(bool isValid, Team team)
    {
        if (isValid)
        {
            if (team == Team.Player)
            {
                return m_validAltPlayer;
            }
            else
            {
                return m_validAltEnemy;
            }
        }
        else
        {
            return m_invalidAlt;
        }
    }

    public static Color GetAttackColor()
    {
        return m_attackColor;
    }

    public static Color GetDefaultTintColor()
    {
        return m_defaultTint;
    }

    public static Color GetStormTintColor()
    {
        return m_stormTint;
    }

    public static Color GetDefaultTintColorForTeam(Team team)
    {
        if (team == Team.Player)
        {
            return m_playerColorTint;
        }
        else
        {
            return m_enemyColorTint;
        }
    }

    public static Color GetDefaultColorForTeam(Team team)
    {
        if (team == Team.Player)
        {
            return m_playerColor;
        }
        else
        {
            return m_enemyColor;
        }
    }

    public static Sprite GetIconCard(string cardName)
    {
        if (m_cardDictionary.ContainsKey(cardName))
        {
            return m_cardDictionary[cardName];
        }

        Sprite loadedSprite = Resources.Load<Sprite>("Cards/" + cardName) as Sprite;
        m_cardDictionary.Add(cardName, loadedSprite);

        return loadedSprite;
    }

    public static Sprite GetIconUnit(string unitName)
    {
        if (m_unitDictionary.ContainsKey(unitName))
        {
            return m_unitDictionary[unitName];
        }

        Sprite loadedSprite = Resources.Load<Sprite>("Units/" + unitName) as Sprite;
        m_unitDictionary.Add(unitName, loadedSprite);

        return loadedSprite;
    }

    public static Sprite GetIconBuilding(string buildingName)
    {
        if (m_buildingDictionary.ContainsKey(buildingName))
        {
            return m_buildingDictionary[buildingName];
        }

        Sprite loadedSprite = Resources.Load<Sprite>("Buildings/" + buildingName) as Sprite;
        m_buildingDictionary.Add(buildingName, loadedSprite);

        return loadedSprite;
    }

    public static Sprite GetIconRelic(string relicName)
    {
        if (m_relicDictionary.ContainsKey(relicName))
        {
            return m_relicDictionary[relicName];
        }

        Sprite loadedSprite = Resources.Load<Sprite>("Relics/" + relicName) as Sprite;
        m_relicDictionary.Add(relicName, loadedSprite);

        return loadedSprite;
    }

    public static Sprite GetIconMap(string mapName)
    {
        if (m_mapDictionary.ContainsKey(mapName))
        {
            return m_mapDictionary[mapName];
        }

        Sprite loadedSprite = Resources.Load<Sprite>("Maps/" + mapName) as Sprite;
        m_mapDictionary.Add(mapName, loadedSprite);

        return loadedSprite;
    }

    public static Sprite GetIconChest(GameElementBase.GameRarity chestRarity)
    {
        if (m_chestRarityDictionary.ContainsKey(chestRarity))
        {
            return m_chestRarityDictionary[chestRarity];
        }

        Sprite loadedSprite;
        if (chestRarity == GameElementBase.GameRarity.Common)
        {
            loadedSprite = Resources.Load<Sprite>("UI2/WorldPerks/Copper Chest") as Sprite;
        }
        else if (chestRarity == GameElementBase.GameRarity.Uncommon)
        {
            loadedSprite = Resources.Load<Sprite>("UI2/WorldPerks/Silver Chest") as Sprite;
        }
        else if (chestRarity == GameElementBase.GameRarity.Rare)
        {
            loadedSprite = Resources.Load<Sprite>("UI2/WorldPerks/Gold Chest") as Sprite;
        }
        else
        {
            Debug.LogError("GetIconChest received chestRarity value that is not handled");
            return null;
        }

        m_chestRarityDictionary.Add(chestRarity, loadedSprite);

        return loadedSprite;
    }

    public static Sprite GetIconAltar(string altarName)
    {
        if (m_altarDictionary.ContainsKey(altarName))
        {
            return m_altarDictionary[altarName];
        }

        Sprite loadedSprite = Resources.Load<Sprite>("UI2/WorldPerks/" + altarName) as Sprite;
        m_altarDictionary.Add(altarName, loadedSprite);

        return loadedSprite;
    }

    public static Sprite GetIconEvent()
    {
        if (m_eventSprite == null)
        {
            m_eventSprite = Resources.Load<Sprite>("UI2/WorldPerks/Event") as Sprite;
        }

        return m_eventSprite;
    }

    public static Sprite GetCardGoldBackground()
    {
        if (m_cardGoldBackgroundSprite == null)
        {
            m_cardGoldBackgroundSprite = Resources.Load<Sprite>("UI2/Cards/GoldCardBackground168x230") as Sprite;
        }

        return m_cardGoldBackgroundSprite;
    }

    public static Sprite GetCardDefaultBackground()
    {
        if (m_cardDefaultBackgroundSprite == null)
        {
            m_cardDefaultBackgroundSprite = Resources.Load<Sprite>("UI2/Cards/CardBackground168x230") as Sprite;
        }

        return m_cardDefaultBackgroundSprite;
    }

    public static Sprite GetIconWorldPerkGold(int goldVal)
    {
        if (m_iconGoldDictionary.ContainsKey(goldVal))
        {
            return m_iconGoldDictionary[goldVal];
        }

        Sprite loadedSprite;
        if (goldVal == Constants.FarGoldVal)
        {
            loadedSprite = Resources.Load<Sprite>("UI2/WorldPerks/MultipleGold") as Sprite;
        }
        else
        {
            loadedSprite = Resources.Load<Sprite>("UI2/WorldPerks/Gold") as Sprite;
        }

        m_iconGoldDictionary.Add(goldVal, loadedSprite);

        return loadedSprite;
    }

    public static Sprite GetWIconChest(GameElementBase.GameRarity chestRarity)
    {
        if (m_chestRarityDictionary.ContainsKey(chestRarity))
        {
            return m_chestRarityDictionary[chestRarity];
        }

        Sprite loadedSprite;
        if (chestRarity == GameElementBase.GameRarity.Common)
        {
            loadedSprite = Resources.Load<Sprite>("UI2/WorldPerks/Copper ChestW") as Sprite;
        }
        else if (chestRarity == GameElementBase.GameRarity.Uncommon)
        {
            loadedSprite = Resources.Load<Sprite>("UI2/WorldPerks/Silver ChestW") as Sprite;
        }
        else if (chestRarity == GameElementBase.GameRarity.Rare)
        {
            loadedSprite = Resources.Load<Sprite>("UI2/WorldPerks/Gold ChestW") as Sprite;
        }
        else
        {
            Debug.LogError("GetWIconChest received chestRarity value that is not handled");
            return null;
        }

        m_chestRarityDictionary.Add(chestRarity, loadedSprite);

        return loadedSprite;
    }

    public static Sprite GetWIconEvent()
    {
        if (m_eventSpriteW == null)
        {
            m_eventSpriteW = Resources.Load<Sprite>("UI2/WorldPerks/EventW") as Sprite;
        }

        return m_eventSpriteW;
    }

    public static Sprite GetIconTerrain(string terrainName)
    {
        if (m_terrainDictionary.ContainsKey(terrainName))
        {
            return m_terrainDictionary[terrainName];
        }

        Sprite loadedSprite = Resources.Load<Sprite>("Terrain/" + terrainName) as Sprite;
        m_terrainDictionary.Add(terrainName, loadedSprite);

        return loadedSprite;
    }

    public static Sprite GetIconIntermissionAction(string actionName)
    {
        if (m_intermissionActionDictionary.ContainsKey(actionName))
        {
            return m_intermissionActionDictionary[actionName];
        }

        Sprite loadedSprite = Resources.Load<Sprite>("Intermission/Action/" + actionName) as Sprite;
        m_intermissionActionDictionary.Add(actionName, loadedSprite);

        return loadedSprite;
    }

    public static void SetMoveableTileForUnit(WorldUnit unit)
    {
        List<GameTile> tilesInMovementRange = WorldGridManager.Instance.GetTilesInMovementRange(unit.GetUnit().GetGameTile(), false, false);

        if (tilesInMovementRange == null)
        {
            return;
        }

        for (int i = 0; i < tilesInMovementRange.Count; i++)
        {
            if (!tilesInMovementRange[i].IsOccupied())
            {
                tilesInMovementRange[i].GetWorldTile().SetMoveable(true);
            }
        }

        List<GameTile> tilesInAttackRange = WorldGridManager.Instance.GetTilesInRangeToMoveAndAttack(unit.GetUnit().GetGameTile(), false, false);

        if (tilesInAttackRange == null)
        {
            return;
        }

        for (int i = 0; i < tilesInAttackRange.Count; i++)
        {
            if (tilesInAttackRange[i].IsOccupied() && tilesInAttackRange[i].GetOccupyingUnit().GetTeam() != unit.GetUnit().GetTeam() && unit.GetUnit().CanHitUnit(tilesInAttackRange[i].GetOccupyingUnit()))
            {
                tilesInAttackRange[i].GetWorldTile().SetAttackable(true);
            }
        }
    }

    public static void SetSpellcraftTiles()
    {
        List<GameUnit> m_unitsWithSpellcraft = new List<GameUnit>();
        List<GameBuildingBase> m_buildingsWithSpellcraft = new List<GameBuildingBase>();
        GamePlayer player = GameHelper.GetPlayer();
        GameOpponent opponent = GameHelper.GetOpponent();
        for (int i = 0; i < player.m_controlledUnits.Count; i++)
        {
            if (player.m_controlledUnits[i].GetSpellcraftKeyword() != null)
            {
                m_unitsWithSpellcraft.Add(player.m_controlledUnits[i]);
            }
        }

        for (int i = 0; i < player.m_controlledBuildings.Count; i++)
        {
            if (player.m_controlledBuildings[i].m_spellcraftBuilding)
            {
                m_buildingsWithSpellcraft.Add(player.m_controlledBuildings[i]);
            }
        }

        for (int i = 0; i < opponent.m_controlledUnits.Count; i++)
        {
            if (opponent.m_controlledUnits[i].GetSpellcraftKeyword() != null)
            {
                m_unitsWithSpellcraft.Add(opponent.m_controlledUnits[i]);
            }
        }

        for (int i = 0; i < m_unitsWithSpellcraft.Count; i++)
        {
            List<GameTile> tilesInSpellcraftRange = WorldGridManager.Instance.GetSurroundingGameTiles(m_unitsWithSpellcraft[i].GetGameTile(), 3, 0);

            if (tilesInSpellcraftRange == null)
            {
                continue;
            }

            for (int c = 0; c < tilesInSpellcraftRange.Count; c++)
            {
                tilesInSpellcraftRange[c].GetWorldTile().AddInSpellcraftRangeCount();
            }
        }


        for (int i = 0; i < m_buildingsWithSpellcraft.Count; i++)
        {
            List<GameTile> tilesInSpellcraftRange = WorldGridManager.Instance.GetSurroundingGameTiles(m_buildingsWithSpellcraft[i].GetGameTile(), 3, 0);

            if (tilesInSpellcraftRange == null)
            {
                continue;
            }

            for (int c = 0; c < tilesInSpellcraftRange.Count; c++)
            {
                tilesInSpellcraftRange[c].GetWorldTile().AddInSpellcraftRangeCount();
            }
        }
    }

    public static void SetBuildingTiles()
    {
        if (GameHelper.IsInLevelBuilder())
        {
            return;
        }

        List<GameBuildingBase> playerRangeBuildings = new List<GameBuildingBase>();
        GamePlayer player = GameHelper.GetPlayer();
        for (int i = 0; i < player.m_controlledBuildings.Count; i++)
        {
            if (player.m_controlledBuildings[i].m_range > 0 && !player.m_controlledBuildings[i].m_isDestroyed)
            {
                playerRangeBuildings.Add(player.m_controlledBuildings[i]);
            }
        }

        for (int i = 0; i < playerRangeBuildings.Count; i++)
        {
            List<GameTile> tilesInBuildingRange = WorldGridManager.Instance.GetSurroundingGameTiles(playerRangeBuildings[i].GetGameTile(), playerRangeBuildings[i].m_range, 0);

            if (tilesInBuildingRange == null)
            {
                continue;
            }

            for (int c = 0; c < tilesInBuildingRange.Count; c++)
            {
                tilesInBuildingRange[c].GetWorldTile().AddInBuildingRangeCount();
            }
        }
    }

    public static void SetAoeTiles(GameUnit unit)
    {
        if (GameHelper.IsInLevelBuilder())
        {
            return;
        }

        if (unit.GetAoeRange() == 0)
        {
            return;
        }

        List<GameTile> tilesInAoeRange = WorldGridManager.Instance.GetSurroundingGameTiles(unit.GetGameTile(), unit.GetAoeRange(), 1);

        if (tilesInAoeRange == null)
        {
            return;
        }

        for (int i = 0; i < tilesInAoeRange.Count; i++)
        {
            tilesInAoeRange[i].GetWorldTile().AddAoeRangeCount();
        }
    }

    public static void ClearSpellcraftTiles()
    {
        WorldGridManager.Instance.ClearAllTilesSpellcraftRange();
    }

    public static void ClearAoeTiles()
    {
        WorldGridManager.Instance.ClearAllTilesAoeRange();
    }

    public static void ClearBuildingTiles()
    {
        WorldGridManager.Instance.ClearAllTilesBuildingRange();
    }

    public static void SelectUnit(WorldUnit unit)
    {
        bool unitAlreadySelected = Globals.m_selectedUnit == unit;

        if (Globals.m_selectedUnit != null)
        {
            UnselectUnit();
        }

        if (!Globals.m_canSelect)
        {
            return;
        }

        if (!unitAlreadySelected)
        {
            UnselectAll();
            Globals.m_selectedUnit = unit;

            SetMoveableTileForUnit(Globals.m_selectedUnit);

            AudioHelper.PlaySFX(AudioHelper.WorldUnitClick);
        }
    }

    public static void SelectEnemy(WorldUnit unit)
    {
        bool enemyAlreadySelected = Globals.m_selectedEnemy == unit;

        if (Globals.m_selectedEnemy != null)
        {
            UnselectEnemy();
        }

        if (!Globals.m_canSelect)
        {
            return;
        }

        if (!enemyAlreadySelected)
        {
            UnselectAll();
            Globals.m_selectedEnemy = unit;

            SetMoveableTileForUnit(Globals.m_selectedEnemy);
        }
    }

    public static void SelectTile(WorldTile tile)
    {
        if (Globals.m_selectedTile == tile)
        {
            Globals.m_selectedTile = null;
            return;
        }

        UnselectAll();
        Globals.m_selectedTile = tile;
        UIHelper.SetBuildingTiles();
    }

    public static void ReselectUnit()
    {
        if (Globals.m_selectedUnit == null)
        {
            return;
        }

        if (!WorldController.Instance.m_isInGame)
        {
            return;
        }

        WorldGridManager.Instance.ClearAllTilesMovementRange();

        SetMoveableTileForUnit(Globals.m_selectedUnit);
    }

    public static void SelectAction(GameActionIntermission action)
    {
        if (!Globals.m_canSelect)
        {
            return;
        }

        if (Globals.m_selectedAction == action)
        {
            Globals.m_selectedAction = null;
        }
        else
        {
            UnselectAll();
            Globals.m_selectedAction = action;
        }
    }

    public static void SelectCard(UICard card)
    {
        if (!Globals.m_canSelect)
        {
            return;
        }

        ClearSpellcraftTiles();

        if (Globals.m_selectedCard == card)
        {
            Globals.m_selectedCard = null;
        }
        else
        {
            UnselectAll();
            Globals.m_selectedCard = card;

            if (Globals.m_selectedCard.m_card.m_targetType != GameCard.Target.None && Globals.m_selectedCard.m_card is GameCardSpellBase)
            {
                SetSpellcraftTiles();
            }
        }
    }

    public static void UnselectUnit()
    {
        if (Globals.m_selectedUnit == null)
        {
            return;
        }

        WorldGridManager.Instance.ClearAllTilesMovementRange();

        Globals.m_selectedUnit = null;
    }

    public static void UnselectEnemy()
    {
        if (Globals.m_selectedEnemy == null)
        {
            return;
        }

        WorldGridManager.Instance.ClearAllTilesMovementRange();

        Globals.m_selectedEnemy = null;
    }

    private static void UnselectAll()
    {
        UnselectEnemy();
        Globals.m_selectedTile = null;
        Globals.m_selectedCard = null;
        Globals.m_selectedAction = null;
        UnselectUnit();

        UIHelper.ClearBuildingTiles();
    }

    private static void CreateWorldElementNotificationImpl(string message, Color color, GameObject positionObj)
    {
        UIWorldElementNotificationController.Instance.AddWorldElementNotification(message, color, positionObj);
    }

    public static void CreateWalletWorldElementNotification(int goldToAdd)
    {
        GameObject walletObj = GameObject.Find("WalletNotifier");

        if (walletObj == null)
        {
            return;
        }

        CreateWorldElementNotification("+" + goldToAdd + " gold!", true, walletObj);
    }

    public static void CreateDeckWorldElementNotification(string message)
    {
        GameObject deckObj = GameObject.Find("DeckNotifier");

        if (deckObj == null)
        {
            return;
        }

        CreateWorldElementNotification(message, true, deckObj);
    }

    public static void CreateWorldElementNotification(string message, bool isPositive, GameObject positionObj)
    {
        Color color = Color.black;

        if (isPositive)
        {
            color = m_valid;
        }
        else
        {
            color = m_invalid;
        }

        CreateWorldElementNotificationImpl(message, color, positionObj);
    }

    public static void CreateMousePointerNotification(string message, bool isPositive)
    {
        Color color = Color.black;

        if (isPositive)
        {
            color = m_valid;
        }
        else
        {
            color = m_invalid;
        }

        UIWorldElementNotification elementNotification = FactoryManager.Instance.GetFactory<UIWorldElementNotificationFactory>().CreateObject<UIWorldElementNotification>(message, color, Input.mousePosition);
    }

    public static void TriggerRelicAnimation<T>()
    {
        UIRelicController.Instance.TriggerRelicAnimation<T>();
    }

    public static UISimpleTooltip CreateSimpleTooltip(string name, string desc)
    {
        return FactoryManager.Instance.GetFactory<UISimpleTooltipFactory>().CreateObject<UISimpleTooltip>(name, desc);
    }

    public static UISimpleTooltip CreateSimpleTooltip(string name, string desc, Team team)
    {
        return FactoryManager.Instance.GetFactory<UISimpleTooltipFactory>().CreateObject<UISimpleTooltip>(name, desc, team);
    }

    public static UISimpleTooltip CreateSimpleTooltip(string name, string desc, bool isValid)
    {
        return FactoryManager.Instance.GetFactory<UISimpleTooltipFactory>().CreateObject<UISimpleTooltip>(name, desc, isValid);
    }

    public static void CreateUnitTooltip(GameUnit unit, bool secondStack = false)
    {
        GameCard cardFromUnit = GameCardFactory.GetCardFromUnit(unit);
        CreateCardTooltip(cardFromUnit, secondStack);
    }

    public static void CreateBigUnitTooltip(GameUnit unit, bool secondStack = false)
    {
        GameCard cardFromUnit = GameCardFactory.GetCardFromUnit(unit);
        CreateBigCardTooltip(cardFromUnit, secondStack);
    }

    public static void CreateCardTooltip(GameCard toShow, bool secondStack = false)
    {
        UICard obj = FactoryManager.Instance.GetFactory<UICardTooltipFactory>().CreateObject<UICard>(toShow, UICard.CardDisplayType.Tooltip);

        if (secondStack)
        {
            UITooltipController.Instance.AddTooltipToSecondStack(obj.GetCardTooltip());
        }
        else
        {
            UITooltipController.Instance.AddTooltipToStack(obj.GetCardTooltip());
        }
    }

    public static void CreateBigCardTooltip(GameCard toShow, bool secondStack = false)
    {
        UICardBigTooltipFactory fact = FactoryManager.Instance.GetFactory<UICardBigTooltipFactory>();

        UICard obj = fact.CreateObject<UICard>(toShow, UICard.CardDisplayType.LargeTooltip);

        if (secondStack)
        {
            UITooltipController.Instance.AddTooltipToSecondStack(obj.GetCardTooltip());
        }
        else
        {
            UITooltipController.Instance.AddTooltipToStack(obj.GetCardTooltip());
        }
    }

    public static void CreateSpellTooltip(GameCard card)
    {
        UICard obj = FactoryManager.Instance.GetFactory<UICardTooltipFactory>().CreateObject<UICard>(card, UICard.CardDisplayType.Tooltip);

        UITooltipController.Instance.AddTooltipToStack(obj.GetCardTooltip());
    }

    public static void CreateWorldPerkTooltip(GameWorldPerk perk)
    {
        if (GameHelper.IsInLevelBuilder())
        {
            return;
        }
        
        if (perk.IsChest())
        {
            UITooltipController.Instance.AddTooltipToStack(CreateSimpleTooltip(perk.GetChestRarity() + " Chest", "Send troops here to collect a " + perk.GetChestRarity() + " relic!"));
        }
        else if (perk.IsEvent())
        {
            string optionOneString = perk.GetEvent().GetOptionOneTooltip();
            string optionTwoString = perk.GetEvent().GetOptionTwoTooltip();

            UITooltipController.Instance.AddTooltipToStack(CreateSimpleTooltip(perk.GetEvent().GetName(), perk.GetEvent().m_eventDesc));
            if (optionOneString != "")
            {
                UITooltipController.Instance.AddTooltipToSecondStack(CreateSimpleTooltip("Option 1", perk.GetEvent().GetOptionOneTooltip()));
            }
            if (optionTwoString != "")
            {
                UITooltipController.Instance.AddTooltipToSecondStack(CreateSimpleTooltip("Option 2", perk.GetEvent().GetOptionTwoTooltip()));
            }
        }
        else if (perk.IsAltar())
        {
            UITooltipController.Instance.AddTooltipToStack(CreateSimpleTooltip(perk.GetAltar().GetName(), perk.GetAltar().m_eventDesc));
            CreateRelicTooltip(perk.GetAltar().GetAltarRelic(), true);
        }
        else if (perk.IsGold())
        {
            UITooltipController.Instance.AddTooltipToStack(CreateSimpleTooltip("Gold", "Send troops here to pick up " + perk.GetGoldVal() + " gold."));
        }
    }

    public static string GetMagicPowerColoredValue(string val)
    {
        return $"<color=#027FFF>{val}</color>";
    }

    public static void CreateChaosTooltipStack()
    {
        for (int i = 1; i < Globals.m_curChaos+1; i++)
        {
            CreateChaosTooltip(i);
        }
    }

    public static void CreateChaosTooltip(int chaosVal)
    {
        if (chaosVal == 0)
        {
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("No Chaos", "No adjustments to gameplay."));
            return;
        }

        string descText = GetChaosDesc(chaosVal);

        UITooltipController.Instance.AddTooltipToSecondStack(UIHelper.CreateSimpleTooltip("Chaos Level " + chaosVal, descText));
    }

    public static string GetChaosDesc(int chaosVal)
    {
        if (chaosVal == 1)
        {
            return "Start with an extra 3 random spell cards and an extra 1 random unit card";
        }
        if (chaosVal == 2)
        {
            return "Map events will shake up the area.";
        }
        if (chaosVal == 3)
        {
            return "Normal enemies are much stronger.";
        }
        if (chaosVal == 4)
        {
            return "Normal enemies have new abilities.";
        }
        if (chaosVal == 5)
        {
            return "Face the true version of bosses and elites.";
        }

        return "";
    }

    public static void CreateHUDNotification(string title, string desc)
    {
        UIHUDNotificationController.Instance.AddNotification(title, desc);
    }

    public static void CreateRelicTooltip(GameRelic relic, bool secondStack = false)
    {
        if (secondStack)
        {
            UITooltipController.Instance.AddTooltipToSecondStack(UIHelper.CreateSimpleTooltip(relic.GetName(), relic.GetDesc()));
        }
        else
        {
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(relic.GetName(), relic.GetDesc()));
        }
    }

    public static string GetDifficultyText(MapDifficulty difficulty)
    {
        if (difficulty == MapDifficulty.Introduction)
        {
            return "Difficulty: Introduction";
        }
        else if (difficulty == MapDifficulty.Easy)
        {
            return "Difficulty: Normal";
        }
        else if (difficulty == MapDifficulty.Medium)
        {
            return "Difficulty: Hard";
        }
        else if (difficulty == MapDifficulty.Hard)
        {
            return "Difficulty: Very Hard";
        }

        return "";
    }

    public static Color GetDifficultyTextColor(MapDifficulty difficulty)
    {
        if (difficulty == MapDifficulty.Introduction)
        {
            return m_difficultyNormal;
        }
        else if (difficulty == MapDifficulty.Easy)
        {
            return m_difficultyNormal;
        }
        else if (difficulty == MapDifficulty.Medium)
        {
            return m_difficultyHard;
        }
        else if (difficulty == MapDifficulty.Hard)
        {
            return m_difficultyVeryHard;
        }

        return Color.white;
    }

    public static void TriggerRelicSelect()
    {
        GameElementBase.GameRarity rarity = GameRelicFactory.GetRandomRarity();

        TriggerRelicSelect(rarity);
    }

    public static void TriggerRelicSelect(GameElementBase.GameRarity rarity)
    {
        GameRelic relicOne = GameRelicFactory.GetRandomRelicAtRarity(rarity);
        GameRelic relicTwo = GameRelicFactory.GetRandomRelicAtRarity(rarity, relicOne);

        UIRelicSelectController.Instance.Init(relicOne, relicTwo);
    }

    public static bool UIShouldBlockClick()
    {
        bool isOverTaggedElement = false;
        if (EventSystem.current.IsPointerOverGameObject())
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                pointerId = -1,
            };

            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0)
            {
                for (int i = 0; i < results.Count; ++i)
                {
                    if (!results[i].gameObject.CompareTag("IgnoreUIBlocker"))
                    {
                        isOverTaggedElement = true;
                    }
                }
            }
        }

        return isOverTaggedElement;
    }

    public static bool CanControlCamera()
    {
        if (GameHelper.IsOpponentsTurn())
        {
            return false;
        }

        if (UILevelSelectController.Instance == null)
        {
            return false;
        }

        if (UILevelSelectController.Instance.m_curMap != null && GameHelper.IsInLevelSelect())
        {
            return false;
        }

        return true;
    }

    public static bool LevelSelectHasMap()
    {
        if (UILevelSelectController.Instance == null)
        {
            return false;
        }

        return UILevelSelectController.Instance.m_curMap != null;
    }

    public static bool GetKeyDown(KeyCode key)
    {
        if (!IsKeyValid())
        {
            return false;
        }

        return Input.GetKeyDown(key);
    }

    public static bool GetKey(KeyCode key)
    {
        if (!IsKeyValid())
        {
            return false;
        }

        return Input.GetKey(key);
    }

    private static bool IsKeyValid()
    {
        if (Constants.DevMode && UICheatConsoleController.Instance.m_consoleHolder.activeSelf)
        {
            return false;
        }

        return true;
    }

    public static string GetHintText()
    {
        GamePlayer player = GameHelper.GetPlayer();

        int endWave = WorldController.Instance.m_gameController.m_currentWaveNumber;
        int numRelics = player.GetRelics().GetRelicListForRead().Count;
        int gold = player.GetGold();
        int maxAttack = 0;
        int maxHealth = 0;

        for (int i = 0; i < player.m_deckBase.Count(); i++)
        {
            if (player.m_deckBase.GetCardByIndex(i) is GameUnitCard)
            {
                GameUnitCard unitCard = (GameUnitCard)player.m_deckBase.GetCardByIndex(i);
                if (unitCard.GetUnit().GetMaxHealth() > maxHealth)
                {
                    maxHealth = unitCard.GetUnit().GetMaxHealth();
                }
                if (unitCard.GetUnit().GetAttack() > maxAttack)
                {
                    maxAttack = unitCard.GetUnit().GetAttack();
                }
            }
        }

        if (endWave >= 4)
        {
            if (numRelics <= 5)
            {
                return "Holding key chokepoints with strong units will allow you to explore outwards in the early game.\n" +
                    "This can help power you up with Relics, Gold, and Events for later waves!";
            }
            else if (numRelics <= 10)
            {
                return "Taking down the elite gives more relics; which are a big power boost!";
            }
            else if (gold >= 250)
            {
                return "Buying buildings with gold can help turn the tide of the battle!";
            }
            else if (maxAttack <= 30)
            {
                return "Use events, spells, and ability triggers like <b>Spellcraft</b> or <b>Enrage</b> to create more powerful units to take on the hordes!";
            }
            else if (maxHealth <= 40)
            {
                return "A set of units with high health can be invaluable in protecting your strong damage dealers!";
            }
        }
        else if (endWave <= 3)
        {
            return "Focus on holding key chokepoints with strong units; while allowing others to go exploring for treasure!";
        }

        return GetRandomGenericHintTextString();
    }

    private static string GetRandomGenericHintTextString()
    {
        return "There are only so many turns in a run; each one is a resource, so use them wisely!";
    }

    public static void TriggerUnitCardSelection()
    {
        GameController gameController = GameHelper.GetGameController();
        List<GameCard> exclusionCards = new List<GameCard>();
        GameCard cardOne;
        GameCard cardTwo;
        GameCard cardThree;

        if (!Globals.loadingRun)
        {
            //Choose unit rarity
            GameElementBase.GameRarity gameRarity = GameHelper.SelectIntermissionUnitRarity();

            cardOne = GameCardFactory.GetRandomStandardUnitCard(gameRarity);
            exclusionCards.Add(cardOne);
            cardTwo = GameCardFactory.GetRandomStandardUnitCard(gameRarity, exclusionCards);
            exclusionCards.Add(cardTwo);

            int exclusionCost = -1;
            if (cardOne.GetCost() == cardTwo.GetCost())
            {
                exclusionCost = cardOne.GetCost();
            }
            bool excludeThreeCostOrHigher = cardOne.GetCost() >= 3 && cardTwo.GetCost() >= 3;

            cardThree = GameCardFactory.GetRandomStandardUnitCard(gameRarity, exclusionCards, exclusionCost, excludeThreeCostOrHigher);

            bool shuffleThirdCard = excludeThreeCostOrHigher || exclusionCost >= 0;
            if (shuffleThirdCard)
            {
                int randomIndex = Random.Range(0, 3);
                GameCard temp;
                switch (randomIndex)
                {
                    case 0:
                        temp = cardOne;
                        cardOne = cardThree;
                        cardThree = temp;
                        break;
                    case 1:
                        temp = cardTwo;
                        cardTwo = cardThree;
                        cardThree = temp;
                        break;
                }
            }

            gameController.m_savedInIntermission = true;
            gameController.m_intermissionSavedCardOne = cardOne;
            gameController.m_intermissionSavedCardTwo = cardTwo;
            gameController.m_intermissionSavedCardThree = cardThree;
            PlayerDataManager.PlayerAccountData.SaveRunData();
            GameNotificationManager.SaveGameDirectorData();
        }
        else
        {
            cardOne = gameController.m_intermissionSavedCardOne;
            exclusionCards.Add(cardOne);
            cardTwo = gameController.m_intermissionSavedCardTwo;
            exclusionCards.Add(cardTwo);
            cardThree = gameController.m_intermissionSavedCardThree;
            Globals.loadingRun = false;
        }

        UICardSelectController.Instance.Init(cardOne, cardTwo, cardThree);
    }

    public static void TriggerSpellCardSelection()
    {
        GameElementBase.GameRarity rarity = GameCardFactory.GetRandomRarity();

        List<GameCard> exclusionCards = new List<GameCard>();
        GameCard cardOne = GameCardFactory.GetRandomStandardSpellCard(rarity);
        exclusionCards.Add(cardOne);
        GameCard cardTwo = GameCardFactory.GetRandomStandardSpellCard(rarity, exclusionCards);
        exclusionCards.Add(cardTwo);

        bool excludeThreeCostOrHigher = cardOne.GetCost() >= 3 && cardTwo.GetCost() >= 3;
        int exclusionCost = -1;
        if (cardOne.GetCost() == cardTwo.GetCost())
        {
            exclusionCost = cardOne.GetCost();
        }
        bool excludeExile = cardOne.m_shouldExile && cardTwo.m_shouldExile;

        GameCard cardThree = GameCardFactory.GetRandomStandardSpellCard(rarity, exclusionCards, exclusionCost, excludeThreeCostOrHigher, excludeExile);

        bool shuffleThirdCard = excludeThreeCostOrHigher || excludeExile || exclusionCost >= 0;
        if (shuffleThirdCard)
        {
            int randomIndex = Random.Range(0, 3);
            GameCard temp;
            switch (randomIndex)
            {
                case 0:
                    temp = cardOne;
                    cardOne = cardThree;
                    cardThree = temp;
                    break;
                case 1:
                    temp = cardTwo;
                    cardTwo = cardThree;
                    cardThree = temp;
                    break;
            }
        }

        UICardSelectController.Instance.Init(cardOne, cardTwo, cardThree);
    }

    public static void TriggerActionSelection()
    {
        List<GameActionIntermission> exclusionActions = new List<GameActionIntermission>();
        GameActionIntermission actionOne = GameIntermissionActionFactory.GetRandomAction(exclusionActions);
        exclusionActions.Add(actionOne);
        GameActionIntermission actionTwo = GameIntermissionActionFactory.GetRandomAction(exclusionActions);
        exclusionActions.Add(actionTwo);
        GameActionIntermission actionThree = GameIntermissionActionFactory.GetRandomAction(exclusionActions);

        UIIntermissionActionSelectController.Instance.Init(actionOne, actionTwo, actionThree);
    }
}
