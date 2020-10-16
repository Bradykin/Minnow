using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ContentUndeadMammoth : GameUnit
{
    private int m_powerBuff = 3;
    private int m_healthBuff = 10;

    public ContentUndeadMammoth()
    {
        m_maxHealth = 12;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_power = 4;

        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_keywordHolder.m_keywords.Add(new GameDeathKeyword(new GameReturnToDeckBuffedAction(this, m_powerBuff, m_healthBuff)));
        m_neverSetIsDead = true;

        m_name = "Undead Mammoth";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }
}

public class GameReturnToDeckBuffedAction : GameAction
{
    private GameUnit m_retuningUnit;
    private int m_powerBuff;
    private int m_healthBuff;

    public GameReturnToDeckBuffedAction(GameUnit returningUnit, int powerBuff, int healthBuff)
    {
        m_retuningUnit = returningUnit;
        m_powerBuff = powerBuff;
        m_healthBuff = healthBuff;
        
        m_name = "Return to Deck Buffed";
        m_desc = "Return to your deck, also giving it +" + m_powerBuff + "/+" + m_healthBuff + ".";
        m_actionParamType = ActionParamType.UnitTwoIntParam;
    }

    public override void DoAction()
    {
        m_retuningUnit.AddPower(m_powerBuff);
        m_retuningUnit.AddMaxHealth(m_healthBuff);

        GameUnitCard cardFromUnit = GameCardFactory.GetCardFromUnit(m_retuningUnit);
        GameHelper.GetPlayer().m_curDeck.AddToDiscard(cardFromUnit);
    }

    public override string SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_powerBuff,
            intValue2 = m_healthBuff
        };

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}