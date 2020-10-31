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

        public static Vector3 GetScreenPositionForUnit(this WorldTile worldTile, WorldUnit worldUnit)
        {
            Vector3 screenPos = GetScreenPosition(worldTile);

            screenPos.x += worldUnit.GetUnit().GetWorldTilePositionAdjustment().x;
            screenPos.y = screenPos.y + 0.71f + worldUnit.GetUnit().GetWorldTilePositionAdjustment().y;
            screenPos.z = -1f + worldUnit.GetUnit().GetWorldTilePositionAdjustment().z;

            return screenPos;
        }
    }
}
