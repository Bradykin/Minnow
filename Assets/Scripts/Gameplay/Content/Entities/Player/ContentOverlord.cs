using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOverlord : GameEntity
{
    public ContentOverlord()
    {
        m_maxHealth = 12;
        m_maxAP = 6;
        m_apRegen = 3;
        m_power = 2;
        m_apToAttack = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;
        m_keywordHolder.m_keywords.Add(new GameFlyingKeyword());
        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(3));

        m_name = "Overlord";
        m_desc = "Spends all AP to attack, deals damage equal to power times AP spent.";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    protected override int GetDamageToDealTo(GameEntity target)
    {
        int damage = GetPower() * (GetCurAP() + m_apToAttack);
        this.SpendAP(GetCurAP());

        return damage;
    }

    protected override int GetDamageToDealTo(GameBuildingBase target)
    {
        int damage = GetPower() * (GetCurAP() + m_apToAttack);
        this.SpendAP(GetCurAP());

        return damage;
    }
}