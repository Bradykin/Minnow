using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameShivNearbyAction : GameAction
{
    private GameUnit m_unit;
    private GameCard m_shivCard;
    private int m_numShivsThrown;
    private List<int> m_shivRanges;

    public GameShivNearbyAction(GameUnit unit, int numShivsThrown, int shivRange)
    {
        m_unit = unit;
        m_shivCard = GameCardFactory.GetCardClone(new ContentShivCard());
        m_numShivsThrown = numShivsThrown;
        m_shivRanges = new List<int>();
        m_shivRanges.Add(shivRange);

        m_name = "Throw Shiv";
        m_actionParamType = ActionParamType.UnitTwoIntParam;
    }

    public override string GetDesc()
    {
        return "Throw " + m_numShivsThrown + " shivs at random nearby enemy units within " + m_shivRanges.Max() + " tiles.";
    }

    public override void DoAction()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        List<GameTile> nearbyTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_unit.GetGameTile(), m_shivRanges.Max());

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

        m_numShivsThrown += tempAction.m_numShivsThrown;
        m_shivRanges.AddRange(tempAction.m_shivRanges);
    }

    public override void SubtractAction(GameAction toAdd)
    {
        GameShivNearbyAction tempAction = (GameShivNearbyAction)toAdd;

        m_numShivsThrown -= tempAction.m_numShivsThrown;
        for (int i = 0; i < tempAction.m_shivRanges.Count; i++)
        {
            m_shivRanges.Remove(tempAction.m_shivRanges[i]);
        }
    }

    public override bool ShouldBeRemoved()
    {
        return m_numShivsThrown <= 0 || m_shivRanges.Count == 0;
    }

    public override JsonActionData SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_numShivsThrown,
            intListValue1 = m_shivRanges
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
