using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSabobot : GameUnit
{
    private int m_explosionDamage = 25;
    
    public ContentSabobot()
    {
        m_worldTilePositionAdjustment = new Vector3(0, -0.3f, 0);

        m_startWithMaxStamina = true;

        m_aoeRange = 2;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;
        AddKeyword(new GameMomentumKeyword(new GameDeathAction(this)), true, false);
        AddKeyword(new GameDeathKeyword(new GameExplodeEnemiesAction(this, m_explosionDamage, m_aoeRange)), true, false);
        AddKeyword(new GameDeathKeyword(new GameReturnToDeckAction(this)), true, false);

        m_name = "Sabobot";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.PunchLight;

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 1;
        m_maxStamina = 4;
        m_staminaRegen = 4;
        m_power = 1;
    }
}