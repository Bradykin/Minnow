using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ContentUndeadMammoth : GameUnit
{
    private int m_powerBuff;
    private int m_healthBuff;

    public ContentUndeadMammoth()
    {
        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_neverSetIsDead = true;

        m_name = "Undead Mammoth";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        SetUnitLevel(GetUnitLevel());

        AddKeyword(new GameDeathKeyword(new GameReturnToDeckBuffedAction(this, m_powerBuff, m_healthBuff)), false);

        LateInit();
    }

    public override void SetUnitLevel(int level)
    {
        base.SetUnitLevel(level);

        m_powerBuff = 3;
        m_healthBuff = 10;

        m_maxHealth = 12;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_power = 4;

        if (m_unitLevel >= 1)
        {
            m_powerBuff = 8;
        }

        if (m_unitLevel >= 2)
        {
            m_maxStamina = 6;
            m_staminaRegen = 6;
        }
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
        m_actionParamType = ActionParamType.UnitTwoIntParam;
    }

    public override void DoAction()
    {
        m_retuningUnit.AddPower(m_powerBuff);
        m_retuningUnit.AddMaxHealth(m_healthBuff);

        GameUnitCard cardFromUnit = GameCardFactory.GetCardFromUnit(m_retuningUnit);
        GameHelper.GetPlayer().m_curDeck.AddToDiscard(cardFromUnit);
    }

    public override void AddAction(GameAction toAdd)
    {
        GameReturnToDeckBuffedAction tempAction = (GameReturnToDeckBuffedAction)toAdd;

        m_powerBuff += tempAction.m_powerBuff;
        m_healthBuff += tempAction.m_healthBuff;
    }

    public override string GetDesc()
    {
        return "Return " + m_retuningUnit.m_name + " to your deck, also giving it +" + m_powerBuff + "/+" + m_healthBuff + ".";
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