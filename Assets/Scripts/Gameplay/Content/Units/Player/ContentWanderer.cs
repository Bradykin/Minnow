using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWanderer : GameUnit
{
    public ContentWanderer() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Wanderer";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.DaggerLight;

        AddKeyword(new GameMomentumKeyword(new GameGainShivAction(1)), true, false);
        AddKeyword(new GameShivKeyword(), true, false);

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 30;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_attack = 15;
    }
}
