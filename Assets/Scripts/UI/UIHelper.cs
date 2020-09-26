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
    public static Color m_defaultFaded = new Color(Color.white.r, Color.white.g, Color.white.b, 0.4f);
    public static Color m_selectedTint = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.3f);
    public static Color m_selectedHarshTint = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 1f);
    public static Color m_validTint = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, 0.3f);
    public static Color m_invalidTint = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
    public static Color m_attackTint = new Color(Color.green.r, Color.green.g, Color.green.b, 0.3f);

    public static Color m_valid = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 1.0f);
    public static Color m_validAltPlayer = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 1.0f);
    public static Color m_validAltEnemy = new Color(Color.red.r, Color.red.g, Color.red.b, 1.0f);
    public static Color m_invalid = new Color(Color.red.r, Color.red.g, Color.red.b, 1.0f);
    public static Color m_invalidAlt = new Color(Color.white.r, Color.white.g, Color.white.b, 1.0f);
    public static Color m_attackColor = new Color(Color.green.r, Color.green.g, Color.green.b, 1.0f);

    public static Color m_playerColorTint = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, 0.2f);
    public static Color m_canPlaceTint = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, 0.6f);
    public static Color m_playerColor = new Color(Color.cyan.r, Color.blue.g, Color.blue.b, 1f);
    public static Color m_enemyColorTint = new Color(Color.red.r, Color.red.g, Color.red.b, 0.2f);
    public static Color m_enemyColor = new Color(Color.red.r, Color.red.g, Color.red.b, 1f);

    public static Color m_difficultyNormal = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 1f);
    public static Color m_difficultyHard = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 1f);
    public static Color m_difficultyVeryHard = new Color(Color.red.r, Color.red.g, Color.red.b, 1f);

    public static Color m_commonRarity = new Color(Color.white.r, Color.white.g, Color.white.b, 0.1f);
    public static Color m_uncommonRarity = new Color(Color.white.r, Color.white.g, Color.white.b, 0.7f);
    public static Color m_rareRarity = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 1.0f);
    public static Color m_noRarity = new Color(Color.white.r, Color.white.g, Color.white.b, 0.0f);

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

    public static Sprite GetIconEntity(string entityName)
    {
        return Resources.Load<Sprite>("Entities/" + entityName) as Sprite;
    }

    public static Sprite GetIconEvent(string eventName)
    {
        return Resources.Load<Sprite>("Events/" + eventName) as Sprite;
    }

    public static Sprite GetIconBuilding(string buildingName)
    {
        return Resources.Load<Sprite>("Buildings/" + buildingName) as Sprite;
    }

    public static Sprite GetIconRelic(string relicName)
    {
        return Resources.Load<Sprite>("Relics/" + relicName) as Sprite;
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

    public static void SetMoveableTileForEntity(UIEntity entity)
    {
        List<GameTile> tilesInMovementRange = WorldGridManager.Instance.GetTilesInMovementRange(entity.GetEntity().GetGameTile(), false, false);

        if (tilesInMovementRange == null)
        {
            return;
        }

        for (int i = 0; i < tilesInMovementRange.Count; i++)
        {
            tilesInMovementRange[i].GetWorldTile().SetMoveable(true);
        }

        List<GameTile> tilesInAttackRange = WorldGridManager.Instance.GetTilesInRangeToMoveAndAttack(entity.GetEntity().GetGameTile(), false, false);

        if (tilesInAttackRange == null)
        {
            return;
        }

        for (int i = 0; i < tilesInAttackRange.Count; i++)
        {
            tilesInAttackRange[i].GetWorldTile().SetAttackable(true);
        }
    }

    public static void SelectEntity(UIEntity entity)
    {
        bool entityAlreadySelected = Globals.m_selectedEntity == entity;

        if (Globals.m_selectedEntity != null)
        {
            UnselectEntity();
        }

        if (!Globals.m_canSelect)
        {
            return;
        }

        if (!entityAlreadySelected)
        {
            UnselectAll();
            Globals.m_selectedEntity = entity;

            SetMoveableTileForEntity(Globals.m_selectedEntity);
        }
    }

    public static void SelectEnemy(UIEntity entity)
    {
        bool enemyAlreadySelected = Globals.m_selectedEnemy == entity;

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
            Globals.m_selectedEnemy = entity;

            SetMoveableTileForEntity(Globals.m_selectedEnemy);
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
    }

    public static void ReselectEntity()
    {
        if (Globals.m_selectedEntity == null)
        {
            return;
        }

        WorldGridManager.Instance.ClearAllTilesMovementRange();

        SetMoveableTileForEntity(Globals.m_selectedEntity);
    }

    public static void SelectCard(UICard card)
    {
        if (!Globals.m_canSelect)
        {
            return;
        }

        if (Globals.m_selectedCard == card)
        {
            Globals.m_selectedCard = null;
        }
        else
        {
            UnselectAll();
            Globals.m_selectedCard = card;
        }
    }

    public static void UnselectEntity()
    {
        if (Globals.m_selectedEntity == null)
        {
            return;
        }

        WorldGridManager.Instance.ClearAllTilesMovementRange();

        Globals.m_selectedEntity = null;
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
        UnselectEntity();
    }

    private static void CreateWorldElementNotificationImpl(string message, Color color, GameObject positionObj)
    {
        FactoryManager.Instance.GetFactory<UIWorldElementNotificationFactory>().CreateObject<UIWorldElementNotification>(message, color, positionObj);
    }

    public static void CreateWalletWorldElementNotification(int goldToAdd)
    {
        GameObject walletObj = GameObject.Find("Wallet");

        if (walletObj == null)
        {
            return;
        }

        UIWallet uiWallet = walletObj.GetComponent<UIWallet>();
        CreateWorldElementNotification("+" + goldToAdd + " gold!", true, uiWallet.gameObject);
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

    public static void CreateEntityTooltip(GameEntity entity, bool secondStack = false)
    {
        GameCard cardFromEntity = GameCardFactory.GetCardFromEntity(entity);
        UICard obj = FactoryManager.Instance.GetFactory<UICardTooltipFactory>().CreateObject<UICard>(cardFromEntity, UICard.CardDisplayType.Tooltip);

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

    public static void CreateChaosTooltipStack()
    {
        for (int i = 1; i < Globals.m_curChaos; i++)
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

        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Chaos Level " + chaosVal, descText));
    }

    public static string GetChaosDesc(int chaosVal)
    {
        if (chaosVal == 1)
        {
            return "Start with an extra 3 random spell cards and an extra 1 random entity card";
        }
        if (chaosVal == 2)
        {
            return "Enemies have +1 AP regen";
        }
        if (chaosVal == 3)
        {
            return "Half chance for uncommmon cards";
        }
        if (chaosVal == 4)
        {
            return "Half chance for rare cards";
        }
        if (chaosVal == 5)
        {
            return "Draw 1 less each turn";
        }
        if (chaosVal == 6)
        {
            return "Double cost buildings";
        }
        if (chaosVal == 7)
        {
            return "Double health enemies";
        }
        if (chaosVal == 8)
        {
            return "Double power enemies";
        }
        if (chaosVal == 9)
        {
            return "1 less action each intermission phase";
        }
        if (chaosVal == 10)
        {
            return "1 less energy";
        }

        return "";
    }

    public static void CreateRelicTooltip(GameRelic relic)
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(relic.m_name, relic.GetDesc()));
    }

    public static string GetDifficultyText(MapDifficulty difficulty)
    {
        if (difficulty == MapDifficulty.Easy)
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
        if (difficulty == MapDifficulty.Easy)
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
}
