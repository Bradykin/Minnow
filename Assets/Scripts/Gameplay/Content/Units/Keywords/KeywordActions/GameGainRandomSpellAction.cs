using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainRandomSpellAction : GameAction
{
    private int m_numTimesToTrigger = 1;

    public GameGainRandomSpellAction(int numTimesToTrigger)
    {
        m_name = "Gain Random Spell";
        m_actionParamType = ActionParamType.IntParam;

        m_numTimesToTrigger = numTimesToTrigger;
    }

    public override string GetDesc()
    {
        if (m_numTimesToTrigger == 1)
        {
            return "Add a random temporary non-exile spell card to your hand.";
        }
        else
        {
            return $"Add {m_numTimesToTrigger} random temporary non-exile spell cards to your hand.";
        }
    }

    public override void DoAction()
    {
        for (int i = 0; i < m_numTimesToTrigger; i++)
        {
            AudioHelper.PlaySFX(AudioHelper.GainRandomSpellCard);
            GameHelper.GetPlayer().AddCardToHand(GameCardFactory.GetRandomStandardSpellCard(GameCardFactory.m_exileSpells), false);
        }
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainRandomSpellAction tempAction = (GameGainRandomSpellAction)toAdd;

        m_numTimesToTrigger += tempAction.m_numTimesToTrigger;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameGainRandomSpellAction tempAction = (GameGainRandomSpellAction)toSubtract;

        m_numTimesToTrigger -= tempAction.m_numTimesToTrigger;
    }

    public override bool ShouldBeRemoved()
    {
        return m_numTimesToTrigger <= 0;
    }

    public override GameUnit GetGameUnit()
    {
        return null;
    }

    public override JsonGameActionData SaveToJson()
    {
        JsonGameActionData jsonData = new JsonGameActionData
        {
            name = m_name,
            intValue1 = m_numTimesToTrigger
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}