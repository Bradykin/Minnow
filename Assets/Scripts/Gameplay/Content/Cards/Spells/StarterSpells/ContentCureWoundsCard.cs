using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCureWoundsCard : GameCardSpellBase
{
    public ContentCureWoundsCard()
    {
        m_name = "Cure Wounds";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Starter;

        InitializeWithLevel(GetCardLevel());

        SetupBasicData();

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        string description = GetHealDescString();

        if (GetCardLevel() >= 2)
        {
            description += "\nTrigger <b>Enrage</b> on the target.\n";
        }

        if (GameHelper.HasRelic<ContentTraditionalMethodsRelic>())
        {
            description += "\nDraw a card.";
        }

        return description;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.Heal(GetSpellValue());

        if (GetCardLevel() >= 2)
        {
            GameEnrageKeyword enrageKeyword = targetUnit.GetEnrageKeyword();

            if (enrageKeyword != null)
            {
                enrageKeyword.DoAction(0);

                //Trigger again if the player has the Bestial Wrath relic
                if (targetUnit.GetTypeline() == Typeline.Monster && targetUnit.GetTeam() == Team.Player)
                {
                    if (GameHelper.HasRelic<ContentBestialWrathRelic>())
                    {
                        enrageKeyword.DoAction(0);
                    }
                }
            }
        }


        if (GameHelper.HasRelic<ContentTraditionalMethodsRelic>())
        {
            GameHelper.GetPlayer().DrawCard();
        }
    }

    public override void InitializeWithLevel(int level)
    {
        m_cost = 1;
        m_spellEffect = 8;

        if (level >= 1)
        {
            m_spellEffect = 20;
        }

        if (level >= 2)
        {
            //Triggers enrage on the unit
        }
    }
}
