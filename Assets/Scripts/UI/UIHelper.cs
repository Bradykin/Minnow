
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using System.IO;

public static class UIHelper
{
    public static Color m_defaultTint = new Color(Color.white.r, Color.white.g, Color.white.b, 0f);
    public static Color m_selectedTint = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.3f);
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

    public static void SetDefaultTintColorCanPlace(SpriteRenderer renderer, bool canPlace)
    {
        if (!canPlace)
        {
            SetDefaultTintColor(renderer);
        }
        else
        {
            SetDefaultTintColor(renderer); //nmartino - Come back to this once I have a solid plan for displaying placeable spaces
        }
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
            Globals.m_selectedEntity = entity;
            Globals.m_selectedCard = null;

            List<GameTile> tilesInRange = WorldGridManager.Instance.GetTilesInMovementRange(Globals.m_selectedEntity.GetEntity().m_curTile, false);

            for (int i = 0; i < tilesInRange.Count; i++)
            {
                tilesInRange[i].m_curTile.SetMoveable(true);
            }
        }
    }

    public static void ReselectEntity()
    {
        if (Globals.m_selectedEntity == null)
        {
            return;
        }

        WorldGridManager.Instance.ClearAllTilesMovementRange();

        List<GameTile> tilesInRange = WorldGridManager.Instance.GetTilesInMovementRange(Globals.m_selectedEntity.GetEntity().m_curTile, false);

        for (int i = 0; i < tilesInRange.Count; i++)
        {
            tilesInRange[i].m_curTile.SetMoveable(true);
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
            UnselectEntity();
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
            UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip(entity.m_name, descString, entity.GetTeam()));
        }

        List<GameKeywordBase> keyWords = entity.GetKeywordHolder().m_keywords;
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

    public static void CreateChaosTooltip()
    {
        string descText = "";
        if (GameHelper.IsValidChaosLevel(1))
        {
            descText += "1 - Start with an extra 3 random spell cards and an extra 1 random entity card\n";
        }
        if (GameHelper.IsValidChaosLevel(2))
        {
            descText += "2 - Enemies have +1 AP regen\n";
        }
        if (GameHelper.IsValidChaosLevel(3))
        {
            descText += "3 - Half chance for uncommmon cards\n";
        }
        if (GameHelper.IsValidChaosLevel(4))
        {
            descText += "4 - Half chance for rare cards\n";
        }
        if (GameHelper.IsValidChaosLevel(5))
        {
            descText += "5 - Draw 1 less each turn\n";
        }
        if (GameHelper.IsValidChaosLevel(6))
        {
            descText += "6 - Double cost buildings\n";
        }
        if (GameHelper.IsValidChaosLevel(7))
        {
            descText += "7 - Double health enemies\n";
        }
        if (GameHelper.IsValidChaosLevel(8))
        {
            descText += "8 - Double power enemies\n";
        }
        if (GameHelper.IsValidChaosLevel(9))
        {
            descText += "9 - 1 less action each intermission phase\n";
        }
        if (GameHelper.IsValidChaosLevel(10))
        {
            descText += "10 - 1 less energy\n";
        }

        UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Chaos Level", descText));
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
}
