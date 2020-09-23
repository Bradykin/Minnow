using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBloodSacrificeCard : GameCardSpellBase
{
    public ContentBloodSacrificeCard()
    {
        m_spellEffect = 1;

        m_name = "Blood Sacrifice";
        m_playDesc = "The sacrifice was for greater power...";
        m_targetType = Target.Ally;
        m_cost = 0;
        m_rarity = GameRarity.Rare;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        string spString = "";
        if (HasSpellPower())
        {
            spString = GetSpellPowerString();
        }

        return "Sacrifice a friendly entity to draw " + m_spellEffect + spString + " card and gain " + m_spellEffect + spString + " energy.\n" + GetModifiedBySpellPowerString();
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
        player.AddEnergy(GetSpellValue());
        player.DrawCards(GetSpellValue());
    }
}
