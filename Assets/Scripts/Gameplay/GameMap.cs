using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : GameElementBase
{
    public int m_id;
    public MapDifficulty m_difficulty;

    protected void Init()
    {
        m_icon = UIHelper.GetIconMap(m_name);
    }
}
