using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentConjuredImp : GameUnit
{
    private bool disableDuplicate = false;
    
    public ContentConjuredImp()
    {
        m_maxHealth = 15;
        m_maxStamina = 4;
        m_staminaRegen = 3;
        m_power = 6;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Conjured Imp";
        m_desc = "When you play this, add a copy of this card to your hand without this ability.";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override string GetDesc()
    {
        if (disableDuplicate)
        {
            return string.Empty;
        }

        return base.GetDesc();
    }

    public void DisableDuplicate()
    {
        disableDuplicate = true;
    }

    public override void OnSummon()
    {
        base.OnSummon();

        if (!disableDuplicate)
        {
            ContentConjuredImp copyImp = (ContentConjuredImp)GameUnitFactory.GetUnitFromJson(this.SaveToJson());
            ContentConjuredImpCard copyImpCard = (ContentConjuredImpCard)GameCardFactory.GetCardFromUnit(copyImp);
            ((ContentConjuredImp)copyImpCard.m_unit).DisableDuplicate();
            copyImpCard.SetDesc(copyImpCard.m_unit.GetDesc());
            GameHelper.GetPlayer().AddCardToHand(copyImpCard, false);
        }
    }
}
