using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Util
{
    public static class WorldTileExtensions
    {
        public static Vector3 GetScreenPosition(this WorldTile worldTile)
        {
            return new Vector3((worldTile.GetGameTile().m_gridPosition.x + worldTile.GetGameTile().m_gridPosition.y * 0.5f - worldTile.GetGameTile().m_gridPosition.y / 2) * Constants.HexagonInnerRadius * 2.0f, worldTile.GetGameTile().m_gridPosition.y * Constants.HexagonOuterRadius * 1.338f, 0.0f);
        }

        public static Vector3 GetScreenPositionForEntity(this WorldTile worldTile)
        {
            Vector3 screenPos = GetScreenPosition(worldTile);

            screenPos.y += 0.71f;
            screenPos.z = 0f;

            return screenPos;
        }
    }
}
