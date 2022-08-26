using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kristofer.exploration
{
    public class TileUtils : MonoBehaviour
    {
        public static bool InsideBounds(Vector3Int positionCell, int minX, int maxX, int minY, int maxY)
        {

            return positionCell.x <= maxX
                   && positionCell.x >= minX
                   && positionCell.y <= maxY
                   && positionCell.y >= minY;

        }

    }
}
