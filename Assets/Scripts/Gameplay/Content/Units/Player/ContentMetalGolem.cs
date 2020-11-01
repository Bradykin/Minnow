using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ContentMetalGolem : GameUnit
{
    private int m_eatingRange;

    public ContentMetalGolem()
    {
        m_worldTilePositionAdjustment = new Vector3(0.1f, 0.3f, 0);

        m_eatingRange = 1;

        m_maxHealth = 25;
        m_maxStamina = 4;
        m_staminaRegen = 4;
        m_power = 5;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Metal Golem";
        m_desc = "At the end of the turn, gain 1 <b>Damage Shield</b> for each mountain in range " + m_eatingRange + ".";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        AddKeyword(new GameTauntKeyword(), false);

        LateInit();
    }

    public override void EndTurn()
    {
        base.EndTurn();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_gameTile, m_eatingRange, 0);

        int numMountains = 0;
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameTerrainBase terrain = surroundingTiles[i].GetTerrain();

            if (terrain.IsMountain())
            {
                numMountains++;
            }
        }

        if (numMountains == 0)
        {
            return;
        }

        AddKeyword(new GameDamageShieldKeyword(numMountains), false);
    }
}