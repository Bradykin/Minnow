using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentConjuredImp : GameEntity
{
    public ContentConjuredImp()
    {
        m_maxHealth = 15;
        m_maxAP = 4;
        m_apRegen = 3;
        m_power = 6;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Conjured Imp";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        GameHelper.GetPlayer().AddScheduledAction(ScheduledActionTime.StartOfTurn, new GameAddEntityCardToHandAction(GameEntityFactory.GetEntityFromJson(this.SaveToJsonAsJson())));
    }
}
