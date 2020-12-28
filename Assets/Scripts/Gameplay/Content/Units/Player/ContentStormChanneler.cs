using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStormChanneler : GameUnit
{
    private int m_bonusMagicPower = 0;

    public ContentStormChanneler() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        AddKeyword(new GameRangeKeyword(2), true, false);
        AddKeyword(new GameSpellcraftKeyword(new GameGainTempMagicPowerAction(1)), true, false);

        m_name = "Storm Channeler";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.SpellAttackMedium;

        LateInit();
    }

    public override void SpellCast(GameCard.Target targetType, GameTile targetTile)
    {
        base.SpellCast(targetType, targetTile);

        m_bonusMagicPower++;
    }

    public override string GetDesc()
    {
        string returnVal = base.GetDesc();

        returnVal += $"Bonus <b>Magic Power</b>: {m_bonusMagicPower}\n";

        return returnVal;
    }

    public override void Die(bool canRevive = true, DamageType damageType = DamageType.None)
    {
        base.Die(canRevive, damageType);

        GameHelper.GetPlayer().m_tempMagicPowerIncrease -= m_bonusMagicPower;
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 8;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 5;

        m_bonusMagicPower = 0;
    }

    public override JsonGameUnitData SaveToJson()
    {
        JsonGameUnitData jsonData = base.SaveToJson();

        jsonData.intValue = m_bonusMagicPower;

        return jsonData;
    }

    public override void LoadFromJson(JsonGameUnitData jsonData)
    {
        base.LoadFromJson(jsonData);

        m_bonusMagicPower = jsonData.intValue;
    }
}