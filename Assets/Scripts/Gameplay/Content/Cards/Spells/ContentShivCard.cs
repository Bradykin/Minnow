using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShivCard : GameCardSpellBase
{
    public ContentShivCard()
    {
        m_spellEffect = 3;

        m_name = "Shiv";
        m_playDesc = "The target is shivved!";
        m_targetType = Target.Entity;
        m_cost = 0;
        m_rarity = GameRarity.Event;
        m_shouldExile = true;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        return GetDamageDescString();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        int apDrain = GameHelper.RelicCount<ContentPoisonedShivsRelic>();

        if (apDrain > 0)
        {
            targetEntity.SpendAP(apDrain);
        }
        targetEntity.GetHit(GetSpellValue());
        if (targetEntity.m_isDead && Globals.m_goldPerShivKill > 0)
        {
            GameHelper.GetPlayer().m_wallet.AddResources(new GameWallet(Globals.m_goldPerShivKill));
        }
    }

    protected override bool CanTriggerSpellPower()
    {
        GamePlayer player = GameHelper.GetPlayer();
        for (int i = 0; i < player.m_controlledEntities.Count; i++)
        {
            if (player.m_controlledEntities[i] is ContentDwarfShivcaster)
            {
                return false;
            }
        }
        return true;
    }

    protected override int GetSpellValue()
    {
        int damage = base.GetSpellValue();

        damage += (3 * GameHelper.RelicCount<ContentBurningShivsRelic>());

        return damage;
    }
}
