using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOverlord : GameUnit
{
    public ContentOverlord()
    {
        m_maxHealth = 12;
        m_maxStamina = 6;
        m_staminaRegen = 3;
        m_power = 2;
        m_staminaToAttack = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;
        m_keywordHolder.m_keywords.Add(new GameFlyingKeyword());
        m_keywordHolder.m_keywords.Add(new GameRangeKeyword(3));

        m_name = "Overlord";
        m_desc = "Spends all Stamina to attack, deals damage equal to power times Stamina spent.";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    protected override int GetDamageToDealTo(GameUnit target)
    {
        int damage = GetPower() * (GetCurStamina() + m_staminaToAttack);
        this.SpendStamina(GetCurStamina());

        return damage;
    }

    protected override int GetDamageToDealTo(GameBuildingBase target)
    {
        int damage = GetPower() * (GetCurStamina() + m_staminaToAttack);
        this.SpendStamina(GetCurStamina());

        return damage;
    }
}