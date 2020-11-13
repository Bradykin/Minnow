using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOrcWarleaderEnemy : GameEnemyUnit
{
    public int m_spawnRange = 3;
    public int m_orcsSpawned = 6;

    public List<GameEnemyUnit> m_survivingOrcs = new List<GameEnemyUnit>();
    
    public ContentOrcWarleaderEnemy(GameOpponent gameOpponent) : base(gameOpponent)
    {
        if (GameHelper.IsValidChaosLevel(Globals.ChaosLevels.BossStrength))
        {
            m_maxHealth = 600;
            m_maxStamina = 7;
            m_staminaRegen = 7;
            m_power = 30;
        }
        else
        {
            m_maxHealth = 250;
            m_maxStamina = 6;
            m_staminaRegen = 5;
            m_power = 15;
        }

        m_team = Team.Enemy;
        m_rarity = GameRarity.Special;
        m_isBoss = true;

        m_name = "Orc Warleader";
        m_desc = $"The final boss. Kill it, and win.\nThis boss arrives with a might warband to destroy you! This unit gets 1 Damage Reduction for each surviving orc in the warband.\n";

        AddKeyword(new GameForestwalkKeyword(), false);

        m_AIGameEnemyUnit.AddAIStep(new AIScanTargetsInRangeStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIChooseTargetToAttackStandardStep(m_AIGameEnemyUnit), true);
        m_AIGameEnemyUnit.AddAIStep(new AIMoveToTargetStandardStep(m_AIGameEnemyUnit), false);
        m_AIGameEnemyUnit.AddAIStep(new AIAttackUntilOutOfStaminaStandardStep(m_AIGameEnemyUnit), false);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_spawnRange);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameTile temp = surroundingTiles[i];
            int randomIndex = UnityEngine.Random.Range(i, surroundingTiles.Count);
            surroundingTiles[i] = surroundingTiles[randomIndex];
            surroundingTiles[randomIndex] = temp;
        }

        int numOrcsSpawned = 0;
        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            if (surroundingTiles[i].IsPassable(this, false))
            {
                GameEnemyUnit newEnemyUnit;
                if (numOrcsSpawned < m_orcsSpawned / 2)
                {
                    newEnemyUnit = GameUnitFactory.GetEnemyUnitClone(new ContentOrcEnemy(null), WorldController.Instance.m_gameController.m_gameOpponent);
                }
                else
                {
                    newEnemyUnit = GameUnitFactory.GetEnemyUnitClone(new ContentOrcShamanEnemy(null), WorldController.Instance.m_gameController.m_gameOpponent);
                }
                m_survivingOrcs.Add(newEnemyUnit);
                surroundingTiles[i].PlaceUnit(newEnemyUnit);
                newEnemyUnit.OnSummon();
                WorldController.Instance.m_gameController.m_gameOpponent.AddControlledUnit(newEnemyUnit);
                numOrcsSpawned++;
                if (numOrcsSpawned >= m_orcsSpawned)
                {
                    break;
                }
            }
        }

        GetWorldTile().ClearSurroundingFog(m_spawnRange);
        UIHelper.CreateHUDNotification("Boss Arrived", "The Orc Warleader and his warband have arrived!");

        GameHelper.GetGameController().m_activeBossUnits.Add(this);
    }

    public override GameDamageReductionKeyword GetDamageReductionKeyword()
    {
        GameDamageReductionKeyword gameDamageReductionKeyword = base.GetDamageReductionKeyword();

        if (m_survivingOrcs.Count == 0)
        {
            return gameDamageReductionKeyword;
        }

        for (int i = m_survivingOrcs.Count - 1; i >= 0; i--)
        {
            if (m_survivingOrcs[i] == null)
            {
                m_survivingOrcs.RemoveAt(i);
                continue;
            }

            if (m_survivingOrcs[i].m_isDead)
            {
                m_survivingOrcs.RemoveAt(i);
                continue;
            }
        }

        if (gameDamageReductionKeyword == null)
        {
            return new GameDamageReductionKeyword(m_survivingOrcs.Count);
        }

        gameDamageReductionKeyword.AddKeyword(new GameDamageReductionKeyword(m_survivingOrcs.Count));

        return gameDamageReductionKeyword;
    }

    public override string GetDesc()
    {
        string descString = m_desc;

        if (!WorldController.Instance.m_gameController.m_map.AllCrystalsDestroyed())
        {
            descString = "<b>Invulnerable:</b> Crystals still remain.\n" + descString;
        }

        return descString;
    }

    public override void Die(bool canRevive = true)
    {
        base.Die(canRevive);

        GameHelper.EndLevel(RunEndType.Win);
    }

    public override bool IsInvulnerable()
    {
        return !WorldController.Instance.m_gameController.m_map.AllCrystalsDestroyed();
    }
}