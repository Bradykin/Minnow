using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDarkWarriorEnemy : GameEnemyEntity
{
    public ContentDarkWarriorEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        m_maxHealth = 60;
        m_maxAP = 9;
        m_apRegen = 5;
        m_power = 9;

        m_team = Team.Enemy;
        m_rarity = GameRarity.Event;
        m_isElite = true;

        m_minWave = 2;

        m_name = "Dark Warrior";
        m_desc = "An elite foe.  Defeat it and gain a relic!";

        m_keywordHolder.m_keywords.Add(new GameMomentumKeyword(new GameHealAction(this, 10)));

        m_AIGameEnemyEntity.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIChooseTargetToAttackStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyEntity));
        m_AIGameEnemyEntity.AddAIStep(new AIAttackUntilOutOfAPStandardStep(m_AIGameEnemyEntity));

        LateInit();
    }

    public override void Die()
    {
        GamePlayer player = GameHelper.GetPlayer();

        player.AddRelic(GameRelicFactory.GetRandomRelic());

        base.Die();
    }
}