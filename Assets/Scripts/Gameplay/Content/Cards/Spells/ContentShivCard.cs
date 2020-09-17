using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShivCard : GameCardSpellBase
{
    private int m_damage;
    
    public ContentShivCard()
    {
        m_damage = 3;

        m_name = "Shiv";
        m_desc = "Shiv the target, dealing 3 damage.";
        m_playDesc = "The target is shivved!";
        m_targetType = Target.Entity;
        m_cost = 0;
        m_rarity = GameRarity.Common;
        m_shouldExile = true;

        SetupBasicData();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        int damage = m_damage;
        int apDrain = GameHelper.RelicCount<ContentPoisonedShivsRelic>();

        damage += (3 * GameHelper.RelicCount<ContentBurningShivsRelic>());

        if (apDrain > 0)
        {
            targetEntity.SpendAP(apDrain);
        }
        targetEntity.GetHit(damage);
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
}
