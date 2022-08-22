using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackToBounds : State
{
    public void Start(Enemy e)
    {
        e.reactionTime = e.maxReactionTime;
        e.spriteRenderer.color = new Color(1f, 1f, 1f);
        e.reactToPlayerWithinRange = false;
    }

    public void Tick(Enemy e)
    {
        e.targetCell = new Vector3Int(e.minX, e.minY);

        e.Move();
    }

    public State Transition(Enemy e)
    {
        var curCell = e.map.WorldToCell(e.transform.position);
        var playerCell = e.map.WorldToCell(e.player.transform.position);
        var distance = Vector3Int.Distance(playerCell, curCell);

        if (distance <= e.aquisitionRange)
        {
            return new ReactToPlayer();
        }
        else if (TileUtil.InsideBounds(curCell, e.minX, e.maxX, e.minY, e.maxY))
        {
            return new GoToRandomPos();
        }

        return null;
    }
}
