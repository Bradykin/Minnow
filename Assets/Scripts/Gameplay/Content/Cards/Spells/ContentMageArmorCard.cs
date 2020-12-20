using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMageArmorCard : GameCardSpellBase
{
    private int m_drVal = 2;

    public ContentMageArmorCard()
    {
        m_name = "Mage Armor";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        m_cost = 0;

        SetupBasicData();

        m_keywordHolder.AddKeyword(new GameDamageReductionKeyword(0));

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override string GetDesc()
    {
        return "Target allied unit gains <b>Damage Reduction</b> " + m_drVal + ".";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameDamageReductionKeyword(m_drVal), false, false);
    }
}
