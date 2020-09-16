using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWildfolk : GameEntity
{
    private List<GameKeywordBase> m_keywords;

    public ContentWildfolk()
    {
        m_keywords = new List<GameKeywordBase>();
        m_keywords.Add(new GameVictoriousKeyword(new GameExplodeAction(this, 25, 3)));
        m_keywords.Add(new GameEnrageKeyword(new GameGainResourceAction(new GameWallet(5))));
        m_keywords.Add(new GameFlyingKeyword());
        m_keywords.Add(new GameMomentumKeyword(new GameGainEnergyAction(1)));
        m_keywords.Add(new GameDeathKeyword(new GameDrawCardAction(3)));
        m_keywords.Add(new GameRangeKeyword(2));
        m_keywords.Add(new GameRegenerateKeyword(3));
        m_keywords.Add(new GameSpellcraftKeyword(new GameGainAPAction(this, 1)));
        m_keywords.Add(new GameKnowledgeableKeyword(new GameFullHealAction(this)));

        m_maxHealth = 10;
        m_maxAP = 5;
        m_apRegen = 2;
        m_power = 4;

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;

        m_name = "Wildfolk";
        m_desc = "When summoned, gain a random keyword.";
        m_typeline = Typeline.Mystic;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        int r = Random.Range(0, m_keywords.Count);

        GameKeywordBase newKeyword = m_keywords[r];
        m_keywordHolder.m_keywords.Add(newKeyword);
        m_keywords.RemoveAt(r);
    }
}
