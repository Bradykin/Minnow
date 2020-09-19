using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarvenSoldier : GameEntity
{
    public ContentDwarvenSoldier()
    {
        m_maxHealth = 8;
        m_maxAP = 4;
        m_apRegen = 3;
        m_power = 4;

        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_name = "Dwarven Soldier";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        int traditionalMethodsCount = GameHelper.RelicCount<ContentTraditionalMethodsRelic>();

        if (traditionalMethodsCount > 0)
        {
            AddPower(traditionalMethodsCount);
            AddMaxHealth(traditionalMethodsCount);
        }
    }
}
