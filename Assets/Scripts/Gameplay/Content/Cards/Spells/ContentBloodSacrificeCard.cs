using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBloodSacrificeCard : GameCardSpellBase
{
    public ContentBloodSacrificeCard()
    {
        m_spellEffect = 1;

        m_name = "Blood Sacrifice";
        m_desc = "Sacrifice a friendly entity to draw 1 card and gain 1 energy (modified by spell power).";
        m_playDesc = "The sacrifice was for greater power...";
        m_targetType = Target.Ally;
        m_cost = 0;
        m_rarity = GameRarity.Rare;

        SetupBasicData();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.Die();

        GamePlayer player = GameHelper.GetPlayer();
        player.AddBonusEnergy(GetSpellValue());
        player.DrawCards(GetSpellValue());
    }
}
