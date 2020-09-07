using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentZombieOutbreakEvent : GameEvent
{
    public ContentZombieOutbreakEvent(GameTile tile)
    {
        m_name = "Zombie Outbreak";
        m_eventDesc = "One of your troops is looking a little ill...";
        m_tile = tile;
        m_rarity = GameRarity.Rare;

        m_optionOne = new GameEventZombieOutbreakOption(m_tile);

        LateInit();
    }
}

public class GameEventZombieOutbreakOption : GameEventOption
{
    private GameTile m_tile;
    private GameEnemyEntity m_zombieEntity;

    public GameEventZombieOutbreakOption(GameTile tile)
    {
        m_zombieEntity = new ContentZombieEnemy(GameHelper.GetOpponent());

        m_tile = tile;
    }

    public override string GetMessage()
    {
        m_message = m_tile.m_occupyingEntity.m_name + " turns into a zombie.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        m_tile.SwapEntity(m_zombieEntity);
        WorldController.Instance.m_gameController.m_gameOpponent.m_controlledEntities.Add(m_zombieEntity);

        EndEvent();
    }
}
