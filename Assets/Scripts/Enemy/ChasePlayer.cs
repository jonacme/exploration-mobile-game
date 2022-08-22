using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : State
{
    public void Start(Enemy e)
    {
        e.reactionTime = e.maxReactionTime;
        e.reactToPlayerWithinRange = false;
    }

    public void Tick(Enemy e)
    {
        var playerCell = e.map.WorldToCell(e.player.transform.position);

        e.targetCell = playerCell;

        e.Move();
    }

    public State Transition(Enemy e)
    {
        var curCell = e.map.WorldToCell(e.transform.position);
        var playerCell = e.map.WorldToCell(e.player.transform.position);
        var distance = Vector3Int.Distance(playerCell, curCell);

        if (distance > e.aquisitionRange)
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
