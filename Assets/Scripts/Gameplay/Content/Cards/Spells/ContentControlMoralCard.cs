using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentControlMoralCard : GameCardSpellBase
{
    public ContentControlMoralCard()
    {
        m_name = "Control Moral";
        m_desc = "Set a units Stamina to 2.";
        m_targetType = Target.Unit;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.UtilitySpell);
        m_tags.AddTag(GameTag.TagType.DamageSpell);
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

        targetUnit.EmptyStamina();
        targetUnit.GainStamina(2);
    }

    protected override void HandleAudio()
    {
        //Left blank intentionally, audio is handled in PlayCard
    }
}
