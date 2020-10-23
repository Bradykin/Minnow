﻿using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfShivcaster : GameUnit
{
    public ContentDwarfShivcaster()
    {
        m_maxHealth = 16;
        m_maxStamina = 5;
        m_staminaRegen = 2;
        m_power = 2;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Dwarf Shivcaster";
        m_desc = "Shivs no longer trigger <b>Spellcraft</b>.";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        AddKeyword(new GameSpellcraftKeyword(new GameShivNearbyAction(this)), false);
        AddKeyword(new GameRangeKeyword(2), false);
        AddKeyword(new GameShivKeyword(), false);

        LateInit();
    }
}

public class GameShivNearbyAction : GameAction
{
    private GameUnit m_unit;
    private GameCard m_shivCard;
    private int m_numShivsThrown = 2;
    private int m_shivRange = 2;

    public GameShivNearbyAction(GameUnit unit)
    {
        m_unit = unit;
        m_shivCard = GameCardFactory.GetCardClone(new ContentShivCard());

        m_name = "Throw Shiv";
        m_actionParamType = ActionParamType.UnitParam;
    }

    public override void DoAction()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        List<GameTile> nearbyTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_unit.GetGameTile(), m_shivRange);

        List<GameUnit> nearbyEnemies = new List<GameUnit>();

        for (int i = 0; i < nearbyTiles.Count; i++)
        {
            if (nearbyTiles[i].IsOccupied() && !nearbyTiles[i].m_occupyingUnit.m_isDead && nearbyTiles[i].m_occupyingUnit.GetTeam() == Team.Enemy)
            {
                nearbyEnemies.Add(nearbyTiles[i].m_occupyingUnit);
            }
        }

        if (nearbyEnemies.Count == 0)
        {
            return;
        }

        for (int i = 0; i < m_numShivsThrown; i++)
        {
            int randomIndex = Random.Range(0, nearbyEnemies.Count);
            m_shivCard.PlayCard(nearbyEnemies[randomIndex]);

            if (nearbyEnemies[randomIndex].m_isDead)
            {
                nearbyEnemies.RemoveAt(randomIndex);
                if (nearbyEnemies.Count == 0)
                {
                    break;
                }
            }
        }
    }

    public override void AddAction(GameAction toAdd)
    {
        GameShivNearbyAction tempAction = (GameShivNearbyAction)toAdd;

        //Use the larger of the two shiv ranges instead of adding them.
        if (tempAction.m_shivRange > m_shivRange)
        {
            m_shivRange = tempAction.m_shivRange;
        }

        m_numShivsThrown += tempAction.m_numShivsThrown;
    }

    public override string GetDesc()
    {
        return "Throw " + m_numShivsThrown + " shivs at random nearby enemy units within " + m_shivRange + " tiles.";
    }

    public override string SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
        };

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}