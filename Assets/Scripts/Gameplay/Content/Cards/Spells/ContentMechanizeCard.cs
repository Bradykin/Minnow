using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMechanizeCard : GameCardSpellBase
{
    public ContentMechanizeCard()
    {
        m_name = "Mechanize";
        m_desc = "Target allied unit loses all current Stamina and <b>permanently</b> gains that much power.";
        m_targetType = Target.Ally;
        m_cost = 0;
        m_rarity = GameRarity.Common;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.MaxStamina);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.StaminaRegen);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilitySpell);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        int curStamina = targetUnit.GetCurStamina();
        targetUnit.SpendStamina(curStamina);
        targetUnit.AddStats(curStamina, 0, true, false);
    }
}