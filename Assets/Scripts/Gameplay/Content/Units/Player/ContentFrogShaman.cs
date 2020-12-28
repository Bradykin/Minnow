using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFrogShaman : GameUnit
{
    public ContentFrogShaman() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        AddKeyword(new GameWaterwalkKeyword(), true, false);
        AddKeyword(new GameRangeKeyword(2), true, false);
        AddKeyword(new GameMomentumKeyword(new GameApplyKeywordToOtherOnMomentumAction(this, new GameBrittleKeyword())), true, false);

        m_name = "Frog Shaman";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.SpellAttackMedium;

        LateInit();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 25;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 9;
    }
}