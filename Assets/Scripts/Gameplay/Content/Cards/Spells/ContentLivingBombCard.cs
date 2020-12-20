using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLivingBombCard : GameCardSpellBase
{
    private int m_range = 3;

    public ContentLivingBombCard()
    {
        m_spellEffect = 20;

        m_name = "Living Bomb";
        m_targetType = Target.Unit;
        m_cost = 2;
        m_rarity = GameRarity.Special;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.DamageSpell);
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Target unit gains '<b>Death</b>: Explode, dealing " + m_spellEffect + mpString + " damage to all units in range " + m_range + ".'\n" + GetModifiedByMagicPowerString();
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        if (targetUnit.GetTeam() == Team.Player)
        {
            AudioHelper.PlaySFX(AudioHelper.SmallBuff);
        }
        else
        {
            AudioHelper.PlaySFX(AudioHelper.SmallDebuff);
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameDeathKeyword(new GameExplodeAction(targetUnit, GetSpellValue(), m_range)), false, false);
    }

    protected override void HandleAudio()
    {
        //Left blank intentionally, audio is handled in PlayCard
    }
}
