using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpawnPool
{
    public List<GameSpawnPoolData> m_spawnPoolDatas;

    public GameSpawnPool(List<GameSpawnPoolData> spawnPoolDatas)
    {
        m_spawnPoolDatas = spawnPoolDatas;
    }
}
