using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGreedyKillCard : GameCardSpellBase
{
    private int m_goldGain = 20;

    public ContentGreedyKillCard()
    {
        m_spellEffect = 3;

        m_name = "Greedy Kill";
        m_targetType = Target.Enemy;
        m_cost = 2;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Gold);

        m_onPlaySFX = AudioHelper.DaggerSwingSpell;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Deal " + m_spellEffect + mpString + " damage to target enemy. If it dies, gain " + m_goldGain + " gold.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.GetHitBySpell(GetSpellValue(), this);

        if (targetUnit.m_isDead)
        {
            GameHelper.GetPlayer().GainGold(m_goldGain, true);
        }
    }
}