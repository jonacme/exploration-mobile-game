using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactToPlayer : State
{
    public void Tick(Enemy e) {
        e.reactToPlayerWithinRange = true;
    }

    public State Transition(Enemy e) {
        var curCell = e.map.WorldToCell(e.transform.position);
        var playerCell = e.map.WorldToCell(e.player.transform.position);
        var distance = Vector3Int.Distance(playerCell, curCell);

        if (e.reactionTime <= 0f)
        {
            return new ChasePlayer();
        }
        else if (distance > e.aquisitionRange)
        {
            if (!TileUtil.InsideBounds(curCell, e.minX, e.maxX, e.minY, e.maxY))
            {
                return new GoBackToBounds();
            }
            else
            {
                return new GoToRandomPos();
            }
        }

        return null;
    }
}
