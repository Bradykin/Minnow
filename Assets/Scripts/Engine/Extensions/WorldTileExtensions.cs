using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Util
{
    public static class WorldTileExtensions
    {
        public static Vector3 GetScreenPosition(this WorldTile worldTile)
        {
            return new Vector3((worldTile.m_gameTile.m_gridPosition.x + worldTile.m_gameTile.m_gridPosition.y * 0.5f - worldTile.m_gameTile.m_gridPosition.y / 2) * Constants.HexagonInnerRadius * 2.0f, worldTile.m_gameTile.m_gridPosition.y * Constants.HexagonOuterRadius * 1.5f, 0.0f);
        }
    }
}
