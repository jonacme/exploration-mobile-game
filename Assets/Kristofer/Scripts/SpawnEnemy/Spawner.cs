using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Kristofer.exploration
{
    public class Spawner : MonoBehaviour
    {
        public GameObject enemyPf;
        public int xPos;
        public int yPos;
        public int enemyCount;
        

        private void Start()
        {
            StartCoroutine(SpawnEnemies());
        }

        IEnumerator SpawnEnemies()
        {
            while(enemyCount < 10)
            {
                xPos = Random.Range(1, 50);
                yPos = Random.Range(1, 31);
                Instantiate(enemyPf, new Vector3(xPos, yPos, transform.position.z), Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                enemyCount += 1;
            }


        }

    }
}

