using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfShivcaster : GameEntity
{
    public ContentDwarfShivcaster()
    {
        m_maxHealth = 30;
        m_maxAP = 6;
        m_apRegen = 3;
        m_power = 2;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Dwarf Shivcaster";
        m_desc = "Shivs no longer trigger spellcraft.\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconEntity(m_name);

        m_keywordHolder.m_keywords.Add(new GameSpellcraftKeyword(new GameShivNearbyAction(this)));
        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(2));

        LateInit();
    }
}

public class GameShivNearbyAction : GameAction
{
    private GameEntity m_entity;
    private GameCard m_shivCard;
    private int m_numShivsThrown = 2;

    public GameShivNearbyAction(GameEntity entity)
    {
        m_entity = entity;
        m_shivCard = GameCardFactory.GetCardClone(new ContentShivCard());

        m_name = "Throw Shiv";
        m_desc = "Throw " + m_numShivsThrown + " shivs at random nearby enemies within two tiles.";
        m_actionParamType = ActionParamType.EntityParam;
    }

    public override void DoAction()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        List<GameTile> nearbyTiles = WorldGridManager.Instance.GetSurroundingTiles(m_entity.GetGameTile(), 2);

        List<GameEntity> nearbyEnemies = new List<GameEntity>();

        for (int i = 0; i < nearbyTiles.Count; i++)
        {
            if (nearbyTiles[i].IsOccupied() && !nearbyTiles[i].m_occupyingEntity.m_isDead && nearbyTiles[i].m_occupyingEntity.GetTeam() == Team.Enemy)
            {
                nearbyEnemies.Add(nearbyTiles[i].m_occupyingEntity);
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

    public override string SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
        };

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
