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
        m_worldTilePositionAdjustment = new Vector3(0.3f, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Starter;

        m_name = "Undead Mammoth";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        InitializeWithLevel(GetUnitLevel());

        AddKeyword(new GameDeathKeyword(new GameReturnToDeckBuffedAction(this, m_powerBuff, m_healthBuff)), false);

        LateInit();
    }

    public override void InitializeWithLevel(int level)
    {
        m_powerBuff = 3;
        m_healthBuff = 10;

        m_maxHealth = 12;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 4;

        if (level >= 1)
        {
            m_powerBuff = 8;
        }

        if (level >= 2)
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
        if (m_retuningUnit.m_returnedToDeckDeath)
        {
            return;
        }
        m_retuningUnit.m_returnedToDeckDeath = true;

        m_retuningUnit.AddStats(m_powerBuff, m_healthBuff);

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
        return "Return " + m_retuningUnit.GetName() + " to your deck, also giving it +" + m_powerBuff + "/+" + m_healthBuff + ".";
    }

    public override JsonActionData SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_powerBuff,
            intValue2 = m_healthBuff
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}