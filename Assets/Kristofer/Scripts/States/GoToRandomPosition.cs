using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Kristofer.exploration
{
    public class GoToRandomPosition : IState
    {


        public void Tick(EnemyScript e)
        {
            var loop = 500;
            do
            {
                var diff = new Vector3Int(Random.Range(-2, 2),
                        Random.Range(-2, 2),
                        0);
                e.targetCell = e.tileMap.WorldToCell(e.transform.position + diff);
                loop--;
                
                if (loop == 0)
                {
                    break;

                }
            }
            while (!TileUtil.InsideBounds(e.targetCell, e.minX, e.maxX, e.minY, e.maxY));
            
            e.Move();
        }

        

        public IState Transition(EnemyScript e)
        {


            var currentCell = e.tileMap.WorldToCell(e.transform.position);

            var playerCell = e.tileMap.WorldToCell(e.player.transform.position);

            var distance = Vector3Int.Distance(playerCell, currentCell);

            if (distance <= e.targetingRange)
            {
                return new ReactToTarget();
            }

            return null;
        }


    }







}






