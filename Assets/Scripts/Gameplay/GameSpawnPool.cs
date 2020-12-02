using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpawnPool
{
    private Dictionary<int, List<GameSpawnPoolData>> m_spawnPoolDatas;

    private float m_chanceSpawn;

    public GameSpawnPool(List<GameSpawnPoolData> spawnPoolDatas, float chanceSpawn = 1.0f)
    {
        m_spawnPoolDatas = new Dictionary<int, List<GameSpawnPoolData>>();
        m_chanceSpawn = chanceSpawn;
        
        for (int i = 1; i < 7; i++)
        {
            m_spawnPoolDatas.Add(i, new List<GameSpawnPoolData>());
        }

        for (int i = 0; i < spawnPoolDatas.Count; i++)
        {
            m_spawnPoolDatas[spawnPoolDatas[i].m_wave].Add(spawnPoolDatas[i]);
        }
    }

    public bool CheckTrySpawn()
    {
        float random = Random.Range(0.0f, 1.0f);
        return m_chanceSpawn >= random;
    }

    public bool TryGetSpawnPoolForWave(int wave, out List<GameSpawnPoolData> spawnPoolDatas)
    {
        if (m_spawnPoolDatas.ContainsKey(wave))
        {
            spawnPoolDatas = m_spawnPoolDatas[wave];
            return true;
        }

        spawnPoolDatas = new List<GameSpawnPoolData>();
        return false;
    }
}
