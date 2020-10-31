using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWildfolk : GameUnit
{
    private List<GameKeywordBase> m_keywords;

    public ContentWildfolk()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.3f, 0);

        m_keywords = new List<GameKeywordBase>();
        m_keywords.Add(new GameVictoriousKeyword(new GameExplodeAction(this, 25, 3)));
        m_keywords.Add(new GameEnrageKeyword(new GameGainResourceAction(new GameWallet(10))));
        m_keywords.Add(new GameFlyingKeyword());
        m_keywords.Add(new GameMomentumKeyword(new GameGainEnergyAction(1)));
        m_keywords.Add(new GameDeathKeyword(new GameDrawCardAction(3)));
        m_keywords.Add(new GameRangeKeyword(2));
        m_keywords.Add(new GameRegenerateKeyword(10));
        m_keywords.Add(new GameSpellcraftKeyword(new GameGainStaminaAction(this, 1)));
        m_keywords.Add(new GameKnowledgeableKeyword(new GameFullHealAction(this)));

        m_maxHealth = 3;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;

        m_name = "Wildfolk";
        m_desc = "When summoned, gain a random keyword and +3/+10.";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        AddStats(3, 10);

        if (m_keywords.Count == 0)
        {
            return;
        }

        int r = Random.Range(0, m_keywords.Count);

        GameKeywordBase newKeyword = m_keywords[r];
        AddKeyword(newKeyword);
        m_keywords.RemoveAt(r);
    }
}
