using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStaminaTrainingCard : GameCardSpellBase
{
    public ContentStaminaTrainingCard()
    {
        m_name = "Stamina Training";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Starter;
        m_shouldExile = true;

        SetCardLevel(GetCardLevel());

        SetupBasicData();
    }

    public override string GetDesc()
    {
        string spString = "";
        if (HasSpellPower())
        {
            spString = GetSpellPowerString();
        }

        if (m_cardLevel >= 2)
        {
            return "Give an allied unit +" + m_spellEffect + spString + " max Stamina.\nFill the target's Stamina.\n" + GetModifiedBySpellPowerString() + "\n\n<i>(Buffs are permanent)</i>";
        }
        else
        {
            return "Give an allied unit +" + m_spellEffect + spString + " max Stamina.\n" + GetModifiedBySpellPowerString() + "\n\n<i>(Buffs are permanent)</i>";
        }
    }

    public override bool PlayerHasUnlockedCard()
    {
        return Constants.CheatsOn || (base.PlayerHasUnlockedCard() && GameMetaProgression.IsChaosLevelAchieved(m_mapUnlockID, 1));
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddMaxStamina(GetSpellValue());

        if (m_cardLevel >= 2)
        {
            targetUnit.FillStamina();
        }
    }

    public override void SetCardLevel(int level)
    {
        base.SetCardLevel(level);

        m_cost = 1;
        m_spellEffect = 1;

        if (m_cardLevel >= 1)
        {
            m_spellEffect = 2;
        }

        if (m_cardLevel >= 2)
        {
            //Also fill curStamina
        }
    }
}
