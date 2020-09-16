using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHomonculus : GameEntity
{
    private int m_explodePower = 25;
    private int m_explodeRange = 3;

    public ContentHomonculus()
    {
        m_maxHealth = 4;
        m_maxAP = 4;
        m_apRegen = 2;
        m_power = 2;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;
        m_keywordHolder.m_keywords.Add(new GameDeathKeyword(new GameExplodeAction(this, m_explodePower, m_explodeRange)));

        m_name = "Homonculus";
        m_desc = "When summoned, draw a card.";
        m_typeline = Typeline.Construct;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        GameHelper.GetPlayer().DrawCard();
    }
}

public class GameExplodeAction : GameAction
{
    private GameEntity m_explodingEntity;
    private int m_explodePower;
    private int m_explodeRange;

    public GameExplodeAction(GameEntity explodingEntity, int explodePower, int explodeRange)
    {
        m_explodingEntity = explodingEntity;
        m_explodePower = explodePower;
        m_explodeRange = explodeRange;

        m_name = "Explode";
        m_desc = "Explode for " + m_explodePower + " damage to all entities and buildings in range " + m_explodeRange;
        m_actionParamType = ActionParamType.EntityTwoIntParam;
    }

    public override void DoAction()
    {
        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingTiles(m_explodingEntity.GetGameTile(), m_explodeRange, 1);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameBuildingBase building = surroundingTiles[i].GetBuilding();
            GameEntity entity = surroundingTiles[i].m_occupyingEntity;

            if (building != null)
            {
                building.GetHit(m_explodePower);
            }

            if (entity != null)
            {
                entity.GetHit(m_explodePower);
            }
        }
    }

    public override string SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_explodePower,
            intValue2 = m_explodeRange
        };

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
