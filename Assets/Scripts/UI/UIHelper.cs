
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
    public static Color m_defaultFaded = new Color(Color.white.r, Color.white.g, Color.white.b, 0.4f);
    public static Color m_selectedTint = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.3f);
    public static Color m_selectedHarshTint = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 1f);
    public static Color m_validTint = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, 0.3f);
    public static Color m_invalidTint = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
    public static Color m_attackTint = new Color(Color.green.r, Color.green.g, Color.green.b, 0.3f);
    public static Color m_spellcraftTint = new Color(128.0f, 0.0f, 128.0f, 0.3f);
    public static Color m_defensiveBuildingTint = new Color(Color.red.r, Color.red.g, Color.red.b, 0.2f);

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
    public static Color m_uncommonRarityTint = new Color(Color.white.r, Color.white.g, Color.white.b, 0.7f);
    public static Color m_rareRarityTint = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 1.0f);
    public static Color m_noRarityTint = new Color(Color.white.r, Color.white.g, Color.white.b, 0.0f);

    public static Color m_commonRarity = new Color(Color.white.r, Color.white.g, Color.white.b, 1f);
    public static Color m_uncommonRarity = new Color(Color.white.r, Color.white.g, Color.white.b, 1f);
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
            return Resources.Load<Sprite>("UI2/Icons/CommonRelicRarityFrame") as Sprite;
        }

        if (rarity == GameElementBase.GameRarity.Uncommon)
        {
            return Resources.Load<Sprite>("UI2/Icons/UncommonRelicRarityFrame") as Sprite;
        }

        if (rarity == GameElementBase.GameRarity.Rare || rarity == GameElementBase.GameRarity.Special)
        {
            return Resources.Load<Sprite>("UI2/Icons/RareRelicRarityFrame") as Sprite;
        }

        return null;
    }

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

    public static Color GetDefensiveBuildingTint(int numBuildings)
    {
        Color returnColor = m_defensiveBuildingTint;
        returnColor.a = returnColor.a + (0.2f * (numBuildings - 1));

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
        return Resources.Load<Sprite>("Cards/" + cardName) as Sprite;
    }

    public static Sprite GetIconUnit(string unitName)
    {
        return Resources.Load<Sprite>("Units/" + unitName) as Sprite;
    }

    public static Sprite GetIconBuilding(string buildingName)
    {
        return Resources.Load<Sprite>("Buildings/" + buildingName) as Sprite;
    }

    public static Sprite GetIconRelic(string relicName)
    {
        return Resources.Load<Sprite>("Relics/" + relicName) as Sprite;
    }

    public static Sprite GetIconMap(string mapName)
    {
        return Resources.Load<Sprite>("Maps/" + mapName) as Sprite;
    }

    public static Sprite GetIconChest(GameElementBase.GameRarity chestRarity)
    {
        if (chestRarity == GameElementBase.GameRarity.Common)
        {
            return Resources.Load<Sprite>("UI/WorldPerks/Copper Chest") as Sprite;
        }
        else if (chestRarity == GameElementBase.GameRarity.Uncommon)
        {
            return Resources.Load<Sprite>("UI/WorldPerks/Silver Chest") as Sprite;
        }
        else if (chestRarity == GameElementBase.GameRarity.Rare)
        {
            return Resources.Load<Sprite>("UI/WorldPerks/Gold Chest") as Sprite;
        }
        return null;
    }

    public static Sprite GetIconAltar(string altarName)
    {
        return Resources.Load<Sprite>("UI/WorldPerks/" + altarName) as Sprite;
    }

    public static Sprite GetIconEvent()
    {
        return Resources.Load<Sprite>("UI/WorldPerks/Event") as Sprite;
    }

    public static Sprite GetIconGold(int goldVal)
    {
        if (goldVal == Constants.FarGoldVal)
        {
            return Resources.Load<Sprite>("UI/MultipleGoldIcon") as Sprite;
        }
        return Resources.Load<Sprite>("UI/GoldIcon") as Sprite;
    }

    public static Sprite GetWIconChest(GameElementBase.GameRarity chestRarity)
    {
        if (chestRarity == GameElementBase.GameRarity.Common)
        {
            return Resources.Load<Sprite>("UI/WorldPerks/Copper ChestW") as Sprite;
        }
        else if (chestRarity == GameElementBase.GameRarity.Uncommon)
        {
            return Resources.Load<Sprite>("UI/WorldPerks/Silver ChestW") as Sprite;
        }
        else if (chestRarity == GameElementBase.GameRarity.Rare)
        {
            return Resources.Load<Sprite>("UI/WorldPerks/Gold ChestW") as Sprite;
        }
        return null;
    }

    public static Sprite GetWIconAltar(string altarName)
    {
        return Resources.Load<Sprite>("UI/WorldPerks/" + altarName + "W") as Sprite;
    }

    public static Sprite GetWIconEvent()
    {
        return Resources.Load<Sprite>("UI/WorldPerks/EventW") as Sprite;
    }

    //Unsafe and currently unused function. Will cause errors if used for tile types without 4 sprite variants
    /*public static Sprite GetRandomIconTerrain(string terrainName)
    {
        int rand = Random.Range(1, 5);

        return Resources.Load<Sprite>("Terrain/" + terrainName + rand) as Sprite;
    }*/

    public static Sprite GetIconTerrain(string terrainName)
    {
        return Resources.Load<Sprite>("Terrain/" + terrainName) as Sprite;
    }

    public static Sprite GetIconIntermissionAction(string actionName)
    {
        return Resources.Load<Sprite>("Intermission/Action/" + actionName) as Sprite;
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
        GamePlayer player = GameHelper.GetPlayer();
        GameOpponent opponent = GameHelper.GetOpponent();
        for (int i = 0; i < player.m_controlledUnits.Count; i++)
        {
            if (player.m_controlledUnits[i].GetSpellcraftKeyword() != null)
            {
                m_unitsWithSpellcraft.Add(player.m_controlledUnits[i]);
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
    }

    public static void SetDefensiveBuildingTiles()
    {
        if (GameHelper.IsInLevelBuilder())
        {
            return;
        }

        List<GameBuildingBase> m_playerDefensiveBuildings = new List<GameBuildingBase>();
        GamePlayer player = GameHelper.GetPlayer();
        for (int i = 0; i < player.m_controlledBuildings.Count; i++)
        {
            if (player.m_controlledBuildings[i].m_buildingType == BuildingType.Defensive && !player.m_controlledBuildings[i].m_isDestroyed)
            {
                m_playerDefensiveBuildings.Add(player.m_controlledBuildings[i]);
            }
        }

        for (int i = 0; i < m_playerDefensiveBuildings.Count; i++)
        {
            List<GameTile> tilesInDefensiveBuildingRange = WorldGridManager.Instance.GetSurroundingGameTiles(m_playerDefensiveBuildings[i].GetGameTile(), m_playerDefensiveBuildings[i].m_range, 0);

            if (tilesInDefensiveBuildingRange == null)
            {
                continue;
            }

            for (int c = 0; c < tilesInDefensiveBuildingRange.Count; c++)
            {
                tilesInDefensiveBuildingRange[c].GetWorldTile().AddInDefensiveBuildingRangeCount();
            }
        }
    }

    public static void ClearSpellcraftTiles()
    {
        WorldGridManager.Instance.ClearAllTilesSpellcraftRange();
    }

    public static void ClearDefensiveBuildingTiles()
    {
        WorldGridManager.Instance.ClearAllTilesDefensiveBuildingRange();
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
        UIHelper.SetDefensiveBuildingTiles();
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
        UnselectUnit();

        UIHelper.ClearDefensiveBuildingTiles();
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

        UICard obj = fact.CreateObject<UICard>(toShow, UICard.CardDisplayType.Tooltip);

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
        int gold = player.m_wallet.m_gold;
        int maxPower = 0;
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
                if (unitCard.GetUnit().GetPower() > maxPower)
                {
                    maxPower = unitCard.GetUnit().GetPower();
                }
            }
        }

        if (endWave >= 4)
        {
            if (numRelics <= 3)
            {
                return "Holding key chokepoints with strong units will allow you to explore outwards in the early game.\n" +
                    "This can help power you up with Relics, Gold, and Events for later waves!";
            }
            else if (numRelics <= 5)
            {
                return "Taking down the elite gives more relics; which are a big power boost!";
            }
            else if (gold >= 250)
            {
                return "Buying buildings with gold can help turn the tide of the battle!";
            }
            else if (maxPower <= 30)
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
}
