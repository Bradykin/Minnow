using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSabobot : GameUnit
{
    private int m_explosionDamage = 25;
    private int m_explosionRange = 1;
    
    public ContentSabobot()
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_maxHealth = 1;
        m_maxStamina = 8;
        m_staminaRegen = 2;
        m_power = 1;
        m_startWithMaxStamina = true;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;
        AddKeyword(new GameMomentumKeyword(new GameDeathAction(this)), false);
        AddKeyword(new GameDeathKeyword(new GameExplodeAction(this, m_explosionDamage, m_explosionRange)), false);

        m_name = "Sabobot";
        m_desc = "Starts at full Stamina.\n";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }
}