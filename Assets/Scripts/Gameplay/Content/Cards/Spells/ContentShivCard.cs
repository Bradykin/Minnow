using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShivCard : GameCardSpellBase
{
    public ContentShivCard()
    {
        m_spellEffect = 4;

        m_name = "Shiv";
        m_targetType = Target.Unit;
        m_cost = 0;
        m_rarity = GameRarity.Event;
        m_shouldExile = true;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Shiv);

        m_audioCategory = AudioHelper.SpellAudioCategory.Damage;
    }

    public override string GetDesc()
    {
        return GetDamageDescString();
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        int staminaDrain = 2 * GameHelper.RelicCount<ContentPoisonedShivsRelic>();

        if (staminaDrain > 0)
        {
            targetUnit.SpendStamina(staminaDrain);
        }

        if (GameHelper.RelicCount<ContentBurningShivsRelic>() > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!targetUnit.m_isDead)
                {
                    base.PlayCard(targetUnit);
                    targetUnit.GetHit(GetSpellValue());
                }
            }
        }
        else
        {
            base.PlayCard(targetUnit);
            targetUnit.GetHit(GetSpellValue());
        }

        if (targetUnit.m_isDead && Globals.m_goldPerShivKill > 0)
        {
            GameHelper.GetPlayer().m_wallet.AddResources(new GameWallet(Globals.m_goldPerShivKill));
        }
    }

    protected override bool CanTriggerSpellPower()
    {
        GamePlayer player = GameHelper.GetPlayer();
        for (int i = 0; i < player.m_controlledUnits.Count; i++)
        {
            if (player.m_controlledUnits[i] is ContentDwarfShivcaster)
            {
                return false;
            }
        }
        return true;
    }
}
