
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using System.IO;

public static class UIHelper
{
    public static Color m_defaultColor = new Color(Color.white.r, Color.white.g, Color.white.b, 1.0f);
    public static Color m_fadedColor = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);

    public static Color m_defaultTint = new Color(Color.white.r, Color.white.g, Color.white.b, 0f);
    public static Color m_selectedTint = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.3f);
    public static Color m_selectedHarshTint = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 1f);
    public static Color m_validTint = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, 0.3f);
    public static Color m_invalidTint = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);

    public static Color m_valid = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 1.0f);
    public static Color m_validAltPlayer = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 1.0f);
    public static Color m_validAltEnemy = new Color(Color.red.r, Color.red.g, Color.red.b, 1.0f);
    public static Color m_invalid = new Color(Color.red.r, Color.red.g, Color.red.b, 1.0f);
    public static Color m_invalidAlt = new Color(Color.white.r, Color.white.g, Color.white.b, 1.0f);

    public static Color m_playerColorTint = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, 0.2f);
    public static Color m_canPlaceTint = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, 0.6f);
    public static Color m_playerColor = new Color(Color.cyan.r, Color.blue.g, Color.blue.b, 1f);
    public static Color m_enemyColorTint = new Color(Color.red.r, Color.red.g, Color.red.b, 0.2f);
    public static Color m_enemyColor = new Color(Color.red.r, Color.red.g, Color.red.b, 1f);

    public static Color m_difficultyNormal = new Color(Color.green.r, Color.green.g, Color.green.b, 1f);
    public static Color m_difficultyHard = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 1f);
    public static Color m_difficultyVeryHard = new Color(Color.red.r, Color.red.g, Color.red.b, 1f);

    public static void SetSelectTintColor(SpriteRenderer renderer, bool isSelected)
    {
        if (isSelected)
        {
            renderer.color = m_selectedTint;
        }
        else
        {
            SetDefaultTintColor(renderer);
        }
    }

    public static void SetSelectHarshTintColor(SpriteRenderer renderer, bool isSelected)
    {
        if (isSelected)
        {
            renderer.color = m_selectedHarshTint;
        }
        else
        {
            SetDefaultTintColor(renderer);
        }
    }

    public static void SetSelectValidTintColor(SpriteRenderer renderer, bool isValid)
    {
        if (isValid)
        {
            renderer.color = m_selectedTint;
        }
        else
        {
            SetValidTintColor(renderer, false);
        }
    }

    public static void SetValidTintColor(SpriteRenderer renderer, bool isValid)
    {
        if (isValid)
        {
            renderer.color = m_validTint;
        }
        else
        {
            renderer.color = m_invalidTint;
        }
    }

    public static void SetValidColor(SpriteRenderer renderer, bool isValid)
    {
        if (isValid)
        {
            renderer.color = m_valid;
        }
        else
        {
            renderer.color = m_invalid;
        }
    }

    public static void SetValidColorAltByTeam(SpriteRenderer renderer, bool isValid, Team team)
    {
        if (isValid)
        {
            if (team == Team.Player)
            {
                renderer.color = m_validAltPlayer;
            }
            else
            {
                renderer.color = m_validAltEnemy;
            }
        }
        else
        {
            renderer.color = m_invalidAlt;
        }
    }

    public static void SetDefaultTintColor(SpriteRenderer renderer)
    {
        renderer.color = m_defaultTint;
    }

    public static void SetDefaultTintColorForTeam(SpriteRenderer renderer, Team team)
    {
        if (team == Team.Player)
        {
            renderer.color = m_playerColorTint;
        }
        else
        {
            renderer.color = m_enemyColorTint;
        }
    }

    public static void SetDefaultColorForTeam(SpriteRenderer renderer, Team team)
    {
        if (team == Team.Player)
        {
            renderer.color = m_playerColor;
        }
        else
        {
            renderer.color = m_enemyColor;
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

    public static Sprite GetIconTerrain(string terrainName, int index)
    {
        return Resources.Load<Sprite>("Terrain/" + terrainName + index) as Sprite;
    }

    public static Sprite GetIconIntermissionAction(string actionName)
    {
        return Resources.Load<Sprite>("Intermission/Action/" + actionName) as Sprite;
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

            List<GameTile> tilesInRange = WorldGridManager.Instance.GetTilesInMovementRange(Globals.m_selectedEntity.GetEntity().m_curTile, false);

            for (int i = 0; i < tilesInRange.Count; i++)
            {
                tilesInRange[i].GetWorldTile().SetMoveable(true);
            }
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

            List<GameTile> tilesInRange = WorldGridManager.Instance.GetTilesInMovementRange(Globals.m_selectedEnemy.GetEntity().m_curTile, false);

            for (int i = 0; i < tilesInRange.Count; i++)
            {
                tilesInRange[i].GetWorldTile().SetMoveable(true);
            }
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

        List<GameTile> tilesInRange = WorldGridManager.Instance.GetTilesInMovementRange(Globals.m_selectedEntity.GetEntity().m_curTile, false);

        if (tilesInRange == null)
        {
            return;
        }

        for (int i = 0; i < tilesInRange.Count; i++)
        {
            tilesInRange[i].GetWorldTile().SetMoveable(true);
        }
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

    private static void CreateWorldElementNotificationImpl(string message, Color color, WorldElementBase worldElement)
    {
        FactoryManager.Instance.GetFactory<UIWorldElementNotificationFactory>().CreateObject<UIWorldElementNotification>(message, color, worldElement);
    }

    public static void CreateWorldElementNotification(string message, bool isPositive, WorldElementBase worldElement)
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

        CreateWorldElementNotificationImpl(message, color, worldElement);
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

    public static void CreateEntityTooltip(GameEntity entity, bool showEntity = true)
    {
        string apString = "+" + entity.GetAPRegen() + " AP/turn";
        string descString = entity.GetDesc() + apString;

        if (showEntity)
        {
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(entity.GetName(), descString, entity.GetTeam()));
        }

        List<GameKeywordBase> keyWords = entity.GetKeywordHolderForRead().m_keywords;
        for (int i = 0; i < keyWords.Count; i++)
        {
            if (showEntity)
            {
                UITooltipController.Instance.AddTooltipToSecondStack(UIHelper.CreateSimpleTooltip(keyWords[i].m_name, keyWords[i].m_desc, entity.GetTeam()));
            }
            else
            {
                UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(keyWords[i].m_name, keyWords[i].m_desc, entity.GetTeam()));
            }
        }
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

    //This is a stub for now.  Can reactivate later if we want it
    public static void CreateTerrainTooltip(GameTerrainBase terrain)
    {
        //if (terrain is ContentGrasslandsTerrain)
        {
            //return;
        }

        //UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(terrain.m_name, terrain.m_desc));
    }

    public static void CreateEventTooltip(GameEvent gameEvent)
    {
        if (!gameEvent.m_isComplete)
        {
            string descString = "An event!  I wonder what happens here...";
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Event", descString));
        }
        UIHelper.CreateTerrainTooltip(gameEvent.m_tile.GetTerrain());
    }

    public static void CreateBuildingTooltip(GameBuildingBase building)
    {
        string title = building.m_name;
        if (building.m_isDestroyed)
        {
            title = "Destroyed " + building.m_name;
        }
        string desc = building.m_desc + "\n" + "Health: " + building.m_curHealth + "/" + building.m_maxHealth;
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(title, desc, Team.Player));
    }

    public static void CreateRelicTooltip(GameRelic relic)
    {
        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(relic.m_name, relic.m_desc));
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
}
