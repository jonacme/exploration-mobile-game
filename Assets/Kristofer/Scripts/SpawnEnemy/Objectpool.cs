using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kristofer.exploration
{
    public class Objectpool : MonoBehaviour
    {
        public GameObject enemyPf;
        public Queue<GameObject> enemyPool = new Queue<GameObject>();
        public int spawnSize = 10;


        private void Start()
        {
            for (int i = 0; i < spawnSize; i++)
            {

                GameObject enemy = Instantiate(enemyPf);
                enemyPool.Enqueue(enemy);
                enemy.SetActive(false);

            }
        }

        public GameObject SpawnEnemy()
        {

            if(enemyPool.Count > 0)
            {

                GameObject enemy = enemyPool.Dequeue();
                enemy.SetActive(true);
                return enemy;

            }
            else
            {

                GameObject enemy = Instantiate(enemyPf);
                return enemy;

            }

        }


        public void ReturnEnemy(GameObject enemy)
        {

            enemyPool.Enqueue(enemy);
            enemy.SetActive(false);

        }



    }



}
