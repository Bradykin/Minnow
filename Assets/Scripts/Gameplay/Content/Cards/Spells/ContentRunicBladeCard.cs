using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRunicBladeCard : GameCardSpellBase
{
    public ContentRunicBladeCard()
    {
        m_name = "Runic Blade";
        m_playDesc = "The blade shimmers with fire!";
        m_targetType = Target.Ally;
        m_cost = 3;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        return "Target ally gains Momentum: Trigger spellcraft.";
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.AddKeyword(new GameMomentumKeyword(new GameSpellcraftAction()));
    }
}


public class GameSpellcraftAction : GameAction
{
    public GameSpellcraftAction()
    {
        m_name = "Spellcraft";
        m_desc = "Spellcraft!";
        m_actionParamType = ActionParamType.NoParams;
    }

    public override void DoAction()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.TriggerSpellcraft();
    }

    public override string SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name
        };

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}