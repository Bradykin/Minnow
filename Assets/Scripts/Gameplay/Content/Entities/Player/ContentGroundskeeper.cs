using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGroundskeeper : GameEntity
{
    private int m_powerBoost = 5;
    private int m_apRegenBoost = 2;

    public ContentGroundskeeper()
    {
        m_maxHealth = 75;
        m_maxAP = 4;
        m_apRegen = 1;
        m_power = 5;


        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Groundskeeper";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconEntity(m_name);

        m_keywordHolder.m_keywords.Add(new GameForestwalkKeyword());

        LateInit();
    }

    public override int GetPower()
    {
        int returnPower = base.GetPower();

        if (m_gameTile == null)
        {
            return returnPower;
        }

        if (m_gameTile.GetTerrain().IsForest())
        {
            returnPower += m_powerBoost;
        }

        return returnPower;
    }

    public override int GetAPRegen()
    {
        int returnAPRegen = base.GetAPRegen();

        if (m_gameTile == null)
        {
            return returnAPRegen;
        }

        if (m_gameTile.GetTerrain().IsForest())
        {
            returnAPRegen += m_apRegenBoost;
        }

        return returnAPRegen;
    }
}