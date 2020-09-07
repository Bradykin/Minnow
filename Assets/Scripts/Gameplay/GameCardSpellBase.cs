using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCardSpellBase : GameCard
{
    protected int m_spellEffect;

    protected void SetupBasicData()
    {
        if (m_shouldExile)
        {
            m_typeline += "Exile ";
        }
        m_typeline += "Spell - " + m_targetType.ToString();
        m_icon = UIHelper.GetIconCard(m_name);
    }

    protected virtual int GetSpellValue()
    {
        int toReturn = m_spellEffect + GameHelper.GetPlayer().GetSpellPower();

        toReturn += 5 * GameHelper.RelicCount<ContentDominerickRefrainRelic>();
        toReturn -= 3 * GameHelper.RelicCount<ContentTomeOfDuluhainRelic>();

        return toReturn;
    }

    public override void PlayCard()
    {
        base.PlayCard();

        TriggerSpellcraft();
    }

    public override void PlayCard(GameBuildingBase targetBuilding)
    {
        base.PlayCard(targetBuilding);

        TriggerSpellcraft();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        base.PlayCard(targetEntity);

        TriggerSpellcraft();
    }

    public override void PlayCard(GameTile targetTile)
    {
        base.PlayCard(targetTile);

        TriggerSpellcraft();
    }

    protected void TriggerSpellcraft()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        for (int i = 0; i < player.m_controlledEntities.Count; i++)
        {
            player.m_controlledEntities[i].SpellCast();
        }
    }
}
